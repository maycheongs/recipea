# AWS S3 Setup Guide

## Changes Made

### 1. **Package Changes**

- Removed: `CloudinaryDotNet`
- Added: `AWSSDK.S3` (version 3.7.401)

### 2. **New Service Created**

- `Recipea/Services/S3Service.cs` - Handles S3 uploads and URL checks

### 3. **Configuration Updates**

- `appsettings.json` - Added AWS configuration section
- `Program.cs` - Registered S3Service instead of CloudinaryService
- `Edit.cshtml.cs` - Updated to use S3Service

### 4. **Removed Files**

- `Recipea/Services/CloudinaryService.cs` - No longer needed

## AWS Setup Steps

### 1. Create S3 Bucket

1. Log into AWS Console
2. Go to S3 service
3. Create a new bucket with name `recipea-images` (or your preferred name)
4. Configure bucket settings:
   - **Region**: Choose your preferred region (default: us-east-1)
   - **Block Public Access**: Disable this to allow public read access to images
   - **Bucket Versioning**: Optional
   - **Default Encryption**: Recommended

### 2. Set Bucket Policy for Public Read Access

Add this bucket policy to allow public read access to your images:

```json
{
  "Version": "2012-10-17",
  "Statement": [
    {
      "Sid": "PublicReadGetObject",
      "Effect": "Allow",
      "Principal": "*",
      "Action": "s3:GetObject",
      "Resource": "arn:aws:s3:::recipea-images/*"
    }
  ]
}
```

Replace `recipea-images` with your bucket name.

### 3. Create IAM User and Access Keys

1. Go to IAM service in AWS Console
2. Create a new user with programmatic access
3. Attach policy `AmazonS3FullAccess` (or create a custom policy with S3 upload permissions)
4. Save the **Access Key ID** and **Secret Access Key**

### 4. Update appsettings.json

Replace the placeholder values with your actual AWS credentials:

```json
{
  "AWS": {
    "AccessKey": "YOUR_ACTUAL_ACCESS_KEY_ID",
    "SecretKey": "YOUR_ACTUAL_SECRET_ACCESS_KEY",
    "Region": "us-east-1",
    "BucketName": "recipea-images"
  }
}
```

**Security Note**: Never commit real credentials to version control. Use environment variables or Azure Key Vault in production.

### 5. Run the Application

```bash
cd Recipea
dotnet run
```

## How It Works

### Upload Process

1. User uploads an image via the Edit page
2. File is processed by `S3Service.UploadImageAsync()`
3. Unique filename is generated: `recipea/filename-timestamp.jpg`
4. File is uploaded to S3 with public read access
5. Public URL is returned and saved to database

### URL Detection

- When editing a recipe with an S3 image, the `IsS3Url()` method checks if the URL is from S3
- If it is an S3 URL, the input field is cleared to prevent displaying the full S3 URL
- This provides a cleaner UX similar to the Cloudinary implementation

## Cost Considerations

### AWS S3 Pricing (approximate)

- **Storage**: $0.023 per GB/month
- **PUT Requests**: $0.005 per 1,000 requests
- **GET Requests**: $0.0004 per 1,000 requests
- **Data Transfer Out**: $0.09 per GB (first 10TB/month)

For a small recipe app with hundreds of images, expect costs of $1-5/month.

### Free Tier

AWS Free Tier includes:

- 5 GB of storage
- 20,000 GET requests
- 2,000 PUT requests
- 15 GB data transfer out

## Benefits Over Cloudinary

1. **Cost Control**: More predictable pricing, especially at scale
2. **Full Control**: Complete control over your data and storage
3. **No Vendor Lock-in**: Easier to migrate if needed
4. **Custom CDN**: Can integrate with CloudFront for global distribution
5. **Flexibility**: Can add image processing with Lambda@Edge or other services

## Optional Enhancements

1. **Image Processing**: Use AWS Lambda + Sharp for automatic thumbnail generation
2. **CDN**: Add CloudFront for faster global image delivery
3. **Backup**: Enable versioning and cross-region replication
4. **Monitoring**: Set up CloudWatch alarms for cost monitoring
