using Gnome.Domain.Interfaces;
using Gnome.Domain.Models;
using MediatR;
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

        public AddVariantCommandHandler(
            IVariantRepository variantRepository,
            ICloudinaryService cloudinaryService)
        {
            _variantRepository = variantRepository;
            _cloudinaryService = cloudinaryService;
        }

        public async Task<int> Handle(AddVariantCommand request, CancellationToken cancellationToken)
        {
            string imageUrl = null;
            if (request.Image != null)
            {
                imageUrl = await _cloudinaryService.UploadImageAsync(request.Image);
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

            var newVariantId = await _variantRepository.AddVariantAsync(variant);
            return newVariantId;
        }
    }
}
