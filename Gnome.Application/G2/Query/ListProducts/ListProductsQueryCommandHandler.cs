using AutoMapper;
using Gnome.Domain.Common;
using Gnome.Domain.Interfaces;
using Gnome.Domain.Models;
using Gnome.Domain.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Gnome.Application.G2.Query.ListProducts
{
    public class ListProductsQueryCommandHandler : IRequestHandler<ListProductsQueryCommand, SortedPagedList<ProductListResponse>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ListProductsQueryCommandHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<SortedPagedList<ProductListResponse>> Handle(ListProductsQueryCommand request, CancellationToken cancellationToken)
        {
            var page = request.Page;
            var pageSize = request.PageSize;
            
            // Create filter from individual properties
            var filter = new ProductFilter
            {
                DateFrom = request.DateFrom,
                DateTo = request.DateTo,
                Name = request.Name,
                Slug = request.Slug,
                MinPlayers = request.MinPlayers,
                MaxPlayers = request.MaxPlayers,
                MinPlayingTime = request.MinPlayingTime,
                MaxPlayingTime = request.MaxPlayingTime,
                Complexity = request.Complexity,
                MinRating = request.MinRating,
                MaxRating = request.MaxRating,
                MinPrice = request.MinPrice,
                MaxPrice = request.MaxPrice,
                CategoryIds = request.CategoryIds,
                CategorySlugs = request.CategorySlugs,
                InStockOnly = request.InStockOnly
            };
            
            var products = await _productRepository.GetProducts(page, pageSize, filter, sortBy: request.SortBy, sortOrder: request.SortOrder);
            var productsCount = await _productRepository.CountProducts(filter);
            int totalPages = (int)Math.Ceiling(productsCount * 1.0 / pageSize);

            return new SortedPagedList<ProductListResponse>
            {
                TotalCount = productsCount,
                Items = products,
                Page = page,
                PageSize = pageSize,
                TotalPages = totalPages,
                SortBy = request.SortBy,
                SortOrder = request.SortOrder
            };
        }
    }
}
