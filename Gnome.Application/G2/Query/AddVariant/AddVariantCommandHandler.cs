using Gnome.Domain.Interfaces;
using Gnome.Domain.Models;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Gnome.Application.G2.Query.AddVariant
{
    public class AddVariantCommandHandler : IRequestHandler<AddVariantCommand, int>
    {
        private readonly IVariantRepository _variantRepository;
        private readonly ICloudinaryService _cloudinaryService;
        private readonly ILogger<AddVariantCommandHandler> _logger;

        public AddVariantCommandHandler(
            IVariantRepository variantRepository,
            ICloudinaryService cloudinaryService,
            ILogger<AddVariantCommandHandler> logger)
        {
            _variantRepository = variantRepository;
            _cloudinaryService = cloudinaryService;
            _logger = logger;
        }

        public async Task<int> Handle(AddVariantCommand request, CancellationToken cancellationToken)
        {
            string imageUrl = null;
            if (request.Image != null)
            {
                try
                {
                    _logger.LogInformation("Starting Cloudinary upload...");
                    imageUrl = await _cloudinaryService.UploadImageAsync(request.Image);
                    _logger.LogInformation("Cloudinary upload successful. URL: {ImageUrl}", imageUrl);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Failed to upload image to Cloudinary");
                    throw;
                }
            }
            else
            {
                _logger.LogWarning("No image provided for variant");
            }

            var variant = new Variant
            {
                Name = request.Name,
                Slug = request.Slug,
                Image = imageUrl,
                Price = request.Price,
                Stock = request.Stock,
                IsPrimary = request.IsPrimary,
                ProductId = request.ProductId,
                CreatedDateTime = DateTime.UtcNow
            };

            _logger.LogInformation("Creating variant with Image URL: {ImageUrl}", imageUrl);

            var newVariantId = await _variantRepository.AddVariantAsync(variant);
            return newVariantId;
        }
    }
}
