using AutoMapper;
using Gnome.Application.G2.Query.ListProducts;
using Gnome.Domain.Common;
using Gnome.Domain.Interfaces;
using Gnome.Domain.Models;
using Gnome.Domain.Responses;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Gnome.Application.G2.Query.AddProduct
{
    public class AddProductCommandHandler : IRequestHandler<AddProductCommand, int>
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IImageRepository _imageRepository;
        private readonly ICloudinaryService _cloudinaryService;
        private readonly IMapper _mapper;
        private readonly ILogger<AddProductCommandHandler> _logger;

        public AddProductCommandHandler(
            IProductRepository productRepository,
            ICategoryRepository categoryRepository,
            IImageRepository imageRepository,
            ICloudinaryService cloudinaryService,
            IMapper mapper,
            ILogger<AddProductCommandHandler> logger)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _imageRepository = imageRepository;
            _cloudinaryService = cloudinaryService;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<int> Handle(AddProductCommand request, CancellationToken cancellationToken)
        {
            var product = new Product
            {
                Name = request.Name,
                Slug = request.Slug,
                Description = request.Description,
                ShortDescription = request.ShortDescription,
                Price = request.Price,
                Stock = request.Stock,
                Rating = request.Rating,
                CreatedDateTime = DateTime.UtcNow
            };

            if (request.CategoryIds.Any())
            {
                foreach (var categoryId in request.CategoryIds)
                {
                    var category = await _categoryRepository.GetCategoryByIdAsync(categoryId);
                    if (category != null)
                    {
                        product.ProductCategories.Add(new ProductCategory
                        {
                            Product = product,
                            Category = category
                        });
                    }
                }
            }            var newProductId = await _productRepository.AddProductAsync(product);

            // Handle image uploads
            if (request.Images != null && request.Images.Any())
            {
                _logger.LogInformation("Processing {ImageCount} images for product {ProductId}", request.Images.Count, newProductId);
                var isFirstImage = true;
                foreach (var imageFile in request.Images)
                {
                    if (imageFile != null && imageFile.Length > 0)
                    {
                        try
                        {
                            _logger.LogInformation("Uploading image {FileName} ({FileSize} bytes) for product {ProductId}", 
                                imageFile.FileName, imageFile.Length, newProductId);
                            
                            var imageUrl = await _cloudinaryService.UploadImageAsync(imageFile);
                            
                            var productImage = new Image
                            {
                                Url = imageUrl,
                                IsPrimary = isFirstImage,
                                ProductId = newProductId,
                                CreatedDateTime = DateTime.UtcNow
                            };

                            await _imageRepository.AddImageAsync(productImage);
                            isFirstImage = false;
                            
                            _logger.LogInformation("Successfully uploaded image for product {ProductId}: {ImageUrl}", newProductId, imageUrl);
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError(ex, "Failed to upload image {FileName} for product {ProductId}", imageFile.FileName, newProductId);
                        }
                    }
                    else
                    {
                        _logger.LogWarning("Skipping null or empty image file for product {ProductId}", newProductId);
                    }
                }
            }
            else
            {
                _logger.LogInformation("No images provided for product {ProductId}", newProductId);
            }

            return newProductId;
        }
    }
}
