using Gnome.Domain.Interfaces;
using Gnome.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Gnome.Application.G2.Query.UpdateVariant
{
    public class UpdateVariantCommandHandler : IRequestHandler<UpdateVariantCommand, int>
    {
        private readonly IVariantRepository _variantRepository;
        private readonly ICloudinaryService _cloudinaryService;

        public UpdateVariantCommandHandler(
            IVariantRepository variantRepository,
            ICloudinaryService cloudinaryService)
        {
            _variantRepository = variantRepository;
            _cloudinaryService = cloudinaryService;
        }

        public async Task<int> Handle(UpdateVariantCommand request, CancellationToken cancellationToken)
        {
            string imageUrl = null;
            if (request.Image != null)
            {
                imageUrl = await _cloudinaryService.UploadImageAsync(request.Image);
            }

            var variant = new Variant
            {
                Id = request.Id,
                Name = request.Name,
                Slug = request.Slug,
                Image = imageUrl,
                Price = request.Price,
                Stock = request.Stock,
                IsPrimary = request.IsPrimary,
                ProductId = request.ProductId
            };

            var updatedVariantId = await _variantRepository.UpdateVariantAsync(variant);
            return updatedVariantId;
        }
    }
} 