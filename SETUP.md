# Recipe App Setup Guide

## Prerequisites

- .NET 8.0 SDK
- AWS Account (for S3 image storage)
- Spoonacular API key (optional, for recipe search)

## First Time Setup

### 1. Clone the Repository

```bash
git clone <repository-url>
cd recipea
```

### 2. Configure API Keys

#### Option A: Using appsettings.Development.json (Recommended for Development)

1. Copy the template file:

   ```bash
   cp Recipea/appsettings.Template.json Recipea/appsettings.Development.json
   ```

2. Edit `Recipea/appsettings.Development.json` and add your API keys:
   ```json
   {
     "Spoonacular": {
       "ApiKey": "your-actual-spoonacular-key"
     },
     "AWS": {
       "AccessKey": "your-actual-aws-access-key",
       "SecretKey": "your-actual-aws-secret-key",
       "Region": "us-east-1",
       "BucketName": "recipea-images"
     }
   }
   ```

#### Option B: Using Environment Variables (Recommended for Production)

Set the following environment variables:

```bash
export Spoonacular__ApiKey="your-spoonacular-key"
export AWS__AccessKey="your-aws-access-key"
export AWS__SecretKey="your-aws-secret-key"
export AWS__Region="us-east-1"
export AWS__BucketName="recipea-images"
```

### 3. Restore Dependencies

```bash
cd Recipea
dotnet restore
```

### 4. Run Database Migrations

```bash
dotnet ef database update
```

### 5. Run the Application

```bash
dotnet run
```

The app will be available at `http://localhost:5217`

## Getting API Keys

### AWS S3 (Required for Image Uploads)

1. Create an AWS account at https://aws.amazon.com
2. Go to S3 service and create a bucket named `recipea-images`
3. Configure bucket for public read access
4. Create an IAM user with S3 access permissions
5. Generate access keys for the IAM user
6. See `AWS_S3_SETUP.md` for detailed instructions

### Spoonacular (Optional - for Recipe Search)

1. Sign up at https://spoonacular.com/food-api
2. Get your free API key
3. Add it to your configuration

## Security Notes

⚠️ **IMPORTANT**: Never commit API keys to version control!

- `appsettings.Development.json` is gitignored
- Use environment variables in production
- Rotate keys if exposed
- Use different keys for development and production

## Troubleshooting

### Database Issues

If you encounter database errors, delete `recipea.db` and run:

```bash
dotnet ef database update
```

### Port Already in Use

If port 5217 is already in use, kill the process:

```bash
lsof -ti:5217 | xargs kill -9
```

Or specify a different port:

```bash
dotnet run --urls="http://localhost:5000"
```

## Development

### Adding a Migration

```bash
dotnet ef migrations add MigrationName
```

### Applying Migrations

```bash
dotnet ef database update
```

### Reverting a Migration

```bash
dotnet ef migrations remove
```
