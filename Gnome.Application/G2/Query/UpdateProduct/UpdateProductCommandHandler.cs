using AutoMapper;
using Gnome.Domain.Interfaces;
using Gnome.Domain.Models;
using MediatR;
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
        private readonly IMapper _mapper;

        public UpdateProductCommandHandler(
            IProductRepository productRepository, 
            ICategoryRepository categoryRepository,
            IMapper mapper)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<int> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = _mapper.Map<Product>(request);
            product.Id = request.Id;

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
            return updatedProductId;
        }
    }
}
