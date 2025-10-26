using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.Extensions.Configuration;

namespace Recipea.Services
{
    public class S3Service
    {
        private readonly IAmazonS3 _s3Client;
        private readonly string _bucketName;
        private readonly string _region;
        private readonly string _baseUrl;

        public S3Service(IConfiguration configuration)
        {
            var accessKey = configuration["AWS:AccessKey"];
            var secretKey = configuration["AWS:SecretKey"];
            _region = configuration["AWS:Region"] ?? "us-east-1";
            _bucketName = configuration["AWS:BucketName"] ?? "recipea-images";
            
            var regionEndpoint = RegionEndpoint.GetBySystemName(_region);
            
            _s3Client = new AmazonS3Client(accessKey, secretKey, regionEndpoint);
            
            // Construct the base URL for public access
            _baseUrl = $"https://{_bucketName}.s3.{_region}.amazonaws.com";
        }

        public async Task<string?> UploadImageAsync(Stream stream, string fileName)
        {
            try
            {
                // Generate a unique file name
                var timestamp = DateTime.UtcNow.ToString("yyyyMMddHHmmss");
                var sanitizedFileName = Path.GetFileNameWithoutExtension(fileName)
                    .Replace(" ", "-")
                    .ToLowerInvariant();
                var extension = Path.GetExtension(fileName).ToLowerInvariant();
                var key = $"recipea/{sanitizedFileName}-{timestamp}{extension}";

                var putRequest = new PutObjectRequest
                {
                    BucketName = _bucketName,
                    Key = key,
                    InputStream = stream,
                    ContentType = GetContentType(extension)
                    // Note: ACLs are disabled, using bucket policy for public access instead
                };

                await _s3Client.PutObjectAsync(putRequest);
                
                // Return the public URL
                return $"{_baseUrl}/{key}";
            }
            catch (Exception ex)
            {
                // Log the exception if you have logging set up
                Console.WriteLine($"S3 Upload Error: {ex.Message}");
                return null;
            }
        }

        public bool IsS3Url(string? url)
        {
            if (string.IsNullOrEmpty(url)) return false;
            return url.Contains(".amazonaws.com") || url.Contains("s3.");
        }

        private string GetContentType(string extension)
        {
            return extension.ToLowerInvariant() switch
            {
                ".jpg" or ".jpeg" => "image/jpeg",
                ".png" => "image/png",
                ".gif" => "image/gif",
                ".webp" => "image/webp",
                _ => "image/jpeg"
            };
        }
    }
}
