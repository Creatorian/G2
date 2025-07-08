using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Gnome.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gnome.Application.Services
{
    public class CloudinaryService : ICloudinaryService
    {
        private readonly Cloudinary _cloudinary;
        private readonly ILogger<CloudinaryService> _logger;

        public CloudinaryService(Cloudinary cloudinary, ILogger<CloudinaryService> logger)
        {
            _cloudinary = cloudinary;
            _logger = logger;
        }

        public async Task<string> UploadImageAsync(IFormFile file)
        {
            if (file == null)
            {
                _logger.LogWarning("UploadImageAsync called with null file");
                return null;
            }

            if (file.Length == 0)
            {
                _logger.LogWarning("UploadImageAsync called with empty file: {FileName}", file.FileName);
                return null;
            }

            try
            {
                using var stream = file.OpenReadStream();
                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(file.FileName, stream),
                    UseFilename = true,
                    UniqueFilename = true,
                    Overwrite = false,
                    Folder = "gnome/products",
                    UploadPreset = "cicija"
                };

                _logger.LogInformation("Uploading to Cloudinary with params: Folder={Folder}, UseFilename={UseFilename}, UniqueFilename={UniqueFilename}, FileName={FileName}, FileSize={FileSize}", 
                    uploadParams.Folder, uploadParams.UseFilename, uploadParams.UniqueFilename, file.FileName, file.Length);

                var uploadResult = await _cloudinary.UploadAsync(uploadParams);

                if (uploadResult.Error != null)
                {
                    _logger.LogError("Cloudinary upload failed: {Error}", uploadResult.Error.Message);
                    throw new InvalidOperationException($"Failed to upload image: {uploadResult.Error.Message}");
                }

                _logger.LogInformation("Cloudinary upload successful. URL: {Url}", uploadResult.SecureUrl);
                return uploadResult.SecureUrl.ToString();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occurred during Cloudinary upload for file: {FileName}", file.FileName);
                throw;
            }
        }

        public async Task<bool> DeleteImageAsync(string imageUrl)
        {
            if (string.IsNullOrEmpty(imageUrl))
            {
                _logger.LogWarning("DeleteImageAsync called with null or empty imageUrl");
                return false;
            }

            try
            {
                // Extract public ID from the URL
                var uri = new Uri(imageUrl);
                var pathSegments = uri.AbsolutePath.Split('/');
                
                // Find the index of 'upload' in the path
                var uploadIndex = Array.IndexOf(pathSegments, "upload");
                if (uploadIndex == -1 || uploadIndex + 2 >= pathSegments.Length)
                {
                    _logger.LogWarning("Invalid Cloudinary URL format: {ImageUrl}", imageUrl);
                    return false;
                }

                // Extract the public ID (everything after 'upload/v1/' or 'upload/')
                var publicIdSegments = pathSegments.Skip(uploadIndex + 2).ToArray();
                var publicId = string.Join("/", publicIdSegments);
                
                // Remove file extension if present
                var lastDotIndex = publicId.LastIndexOf('.');
                if (lastDotIndex > 0)
                {
                    publicId = publicId.Substring(0, lastDotIndex);
                }

                _logger.LogInformation("Deleting image from Cloudinary. Public ID: {PublicId}", publicId);

                var deleteParams = new DeletionParams(publicId);
                var deleteResult = await _cloudinary.DestroyAsync(deleteParams);

                if (deleteResult.Error != null)
                {
                    _logger.LogError("Cloudinary deletion failed: {Error}", deleteResult.Error.Message);
                    return false;
                }

                _logger.LogInformation("Cloudinary deletion successful for public ID: {PublicId}", publicId);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occurred during Cloudinary deletion for URL: {ImageUrl}", imageUrl);
                return false;
            }
        }
    }
}
