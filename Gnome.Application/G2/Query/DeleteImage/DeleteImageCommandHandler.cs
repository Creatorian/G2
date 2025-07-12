using Gnome.Domain.Interfaces;
using Gnome.Domain.Models;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Gnome.Application.G2.Query.DeleteImage
{
    public class DeleteImageCommandHandler : IRequestHandler<DeleteImageCommand, bool>
    {
        private readonly IImageRepository _imageRepository;
        private readonly ICloudinaryService _cloudinaryService;
        private readonly ILogger<DeleteImageCommandHandler> _logger;

        public DeleteImageCommandHandler(
            IImageRepository imageRepository,
            ICloudinaryService cloudinaryService,
            ILogger<DeleteImageCommandHandler> logger)
        {
            _imageRepository = imageRepository;
            _cloudinaryService = cloudinaryService;
            _logger = logger;
        }

        public async Task<bool> Handle(DeleteImageCommand request, CancellationToken cancellationToken)
        {
            // Get the image to delete
            var image = await _imageRepository.GetImageByIdAsync(request.Id);
            if (image == null)
            {
                _logger.LogWarning("Attempted to delete non-existent image {ImageId}", request.Id);
                return false;
            }

            try
            {
                // Delete from Cloudinary if possible
                if (!string.IsNullOrEmpty(image.Url))
                {
                    try
                    {
                        await _cloudinaryService.DeleteImageAsync(image.Url);
                        _logger.LogInformation("Deleted image from Cloudinary: {ImageUrl}", image.Url);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogWarning(ex, "Failed to delete image from Cloudinary: {ImageUrl}", image.Url);
                        // Continue with database deletion even if Cloudinary deletion fails
                    }
                }

                // Delete from database
                var result = await _imageRepository.DeleteImageAsync(request.Id);
                
                if (result)
                {
                    _logger.LogInformation("Successfully deleted image {ImageId} from product {ProductId}", request.Id, image.ProductId);
                }
                
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to delete image {ImageId}", request.Id);
                return false;
            }
        }
    }
} 