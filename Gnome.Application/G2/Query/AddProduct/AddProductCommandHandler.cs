﻿using AutoMapper;
using Gnome.Application.G2.Query.ListProducts;
using Gnome.Domain.Common;
using Gnome.Domain.Interfaces;
using Gnome.Domain.Models;
using Gnome.Domain.Responses;
using MediatR;
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
        private readonly IMapper _mapper;

        public AddProductCommandHandler(
            IProductRepository productRepository,
            IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<int> Handle(AddProductCommand request, CancellationToken cancellationToken)
        {
            var product = new Product
            {
                Name = request.Name,
                Slug = request.Slug,
                Description = request.Description,
                CategoryId = request.CategoryId,
                CreatedDateTime = DateTime.UtcNow
            };

            var newProductId = await _productRepository.AddProductAsync(product);
            return newProductId;
        }
    }
}
