using AutoMapper;
using Gnome.Domain.Common;
using Gnome.Domain.Interfaces;
using Gnome.Domain.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            var products = await _productRepository.GetProducts(page, pageSize, request.Filter, sortBy: request.SortBy, sortOrder: request.SortOrder);
            var productsCount = await _productRepository.CountProducts(request.Filter);
            int totalPages = (int)Math.Ceiling(productsCount * 1.0 / pageSize);

            List<ProductListResponse> productListResponse = _mapper.Map<List<ProductListResponse>>(products);

            return new SortedPagedList<ProductListResponse>
            {
                TotalCount = productsCount,
                Items = productListResponse,
                Page = page,
                PageSize = pageSize,
                TotalPages = totalPages,
                SortBy = request.SortBy,
                SortOrder = request.SortOrder
            };
        }
    }
}
