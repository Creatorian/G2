using Gnome.Domain.Interfaces;
using Gnome.Domain.Models;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Gnome.Application.G2.Query.AddProductImage
{
    public class AddProductImageCommandHandler : IRequestHandler<AddProductImageCommand, bool>
    {
        private readonly IProductRepository _productRepository;
        private readonly IImageRepository _imageRepository;
        private readonly ICloudinaryService _cloudinaryService;
        private readonly ILogger<AddProductImageCommandHandler> _logger;

        public AddProductImageCommandHandler(
            IProductRepository productRepository,
            IImageRepository imageRepository,
            ICloudinaryService cloudinaryService,
            ILogger<AddProductImageCommandHandler> logger)
        {
            _productRepository = productRepository;
            _imageRepository = imageRepository;
            _cloudinaryService = cloudinaryService;
            _logger = logger;
        }

        public async Task<bool> Handle(AddProductImageCommand request, CancellationToken cancellationToken)
        {
            // Verify product exists
            var product = await _productRepository.GetProductByIdAsync(request.ProductId);
            if (product == null)
            {
                _logger.LogWarning("Attempted to add images to non-existent product {ProductId}", request.ProductId);
                return false;
            }

            if (request.Images == null || !request.Images.Any())
            {
                _logger.LogWarning("No images provided for product {ProductId}", request.ProductId);
                return false;
            }

            var uploadedImages = new List<Image>();
            var isFirstImage = true;

            foreach (var imageFile in request.Images)
            {
                if (imageFile != null && imageFile.Length > 0)
                {
                    try
                    {
                        var imageUrl = await _cloudinaryService.UploadImageAsync(imageFile);
                        
                        var productImage = new Image
                        {
                            Url = imageUrl,
                            IsPrimary = request.SetAsPrimary && isFirstImage, // Only set as primary if requested and it's the first image
                            ProductId = request.ProductId,
                            CreatedDateTime = DateTime.UtcNow
                        };

                        await _imageRepository.AddImageAsync(productImage);
                        uploadedImages.Add(productImage);
                        isFirstImage = false;
                        
                        _logger.LogInformation("Uploaded image for product {ProductId}: {ImageUrl}", request.ProductId, imageUrl);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Failed to upload image for product {ProductId}", request.ProductId);
                        // Continue with other images even if one fails
                    }
                }
            }

            return uploadedImages.Any();
        }
    }
} 