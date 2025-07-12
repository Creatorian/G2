using AutoMapper;
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

namespace Gnome.Application.G2.Query.UpdateProduct
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, int>
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IImageRepository _imageRepository;
        private readonly ICloudinaryService _cloudinaryService;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateProductCommandHandler> _logger;

        public UpdateProductCommandHandler(
            IProductRepository productRepository, 
            ICategoryRepository categoryRepository,
            IImageRepository imageRepository,
            ICloudinaryService cloudinaryService,
            IMapper mapper,
            ILogger<UpdateProductCommandHandler> logger)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _imageRepository = imageRepository;
            _cloudinaryService = cloudinaryService;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<int> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("UpdateProductCommand received - Awards count: {AwardsCount}, Awards: {Awards}", 
                request.Awards?.Count ?? 0, 
                request.Awards != null ? string.Join(",", request.Awards) : "null");

            var product = new Product
            {
                Id = request.Id,
                Name = request.Name,
                Slug = request.Slug,
                Description = request.Description,
                ShortDescription = request.ShortDescription,
                NumberOfPlayers = request.NumberOfPlayers,
                PlayingTime = request.PlayingTime,
                CommunityAge = request.CommunityAge,
                Complexity = request.Complexity,
                Price = request.Price,
                Awards = request.Awards != null ? string.Join(",", request.Awards) : null,
                Stock = request.Stock,
                Rating = request.Rating
            };

            _logger.LogInformation("Product Awards field set to: {Awards}", product.Awards);

            // Handle many-to-many relationship
            if (request.CategoryIds != null && request.CategoryIds.Any())
            {
                product.ProductCategories = new List<ProductCategory>();
                foreach (var categoryId in request.CategoryIds)
                {
                    var category = await _categoryRepository.GetCategoryByIdAsync(categoryId);
                    if (category != null)
                    {
                        product.ProductCategories.Add(new ProductCategory
                        {
                            ProductId = product.Id,
                            CategoryId = categoryId
                        });
                    }
                }
            }

            var updatedProductId = await _productRepository.UpdateProductAsync(product);

            // Handle image uploads
            if (request.Images != null && request.Images.Any())
            {
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
                                IsPrimary = false, // New images are not primary by default
                                ProductId = updatedProductId,
                                CreatedDateTime = DateTime.UtcNow
                            };

                            await _imageRepository.AddImageAsync(productImage);
                            
                            _logger.LogInformation("Uploaded additional image for product {ProductId}: {ImageUrl}", updatedProductId, imageUrl);
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError(ex, "Failed to upload additional image for product {ProductId}", updatedProductId);
                            // Continue with other images even if one fails
                        }
                    }
                }
            }

            return updatedProductId;
        }
    }
}
