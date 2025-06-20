﻿using AutoMapper;
using Gnome.Domain.Common;
using Gnome.Domain.Interfaces;
using Gnome.Domain.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Gnome.Application.G2.Query.ListCategories
{
    public class ListCategoriesQueryCommandHandler : IRequestHandler<ListCategoriesQueryCommand, SortedPagedList<CategoryListResponse>>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public ListCategoriesQueryCommandHandler(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<SortedPagedList<CategoryListResponse>> Handle(ListCategoriesQueryCommand request, CancellationToken cancellationToken)
        {
            var page = request.Page;
            var pageSize = request.PageSize;
            var categories = await _categoryRepository.GetCategories(page, pageSize, request.DateFrom, request.DateTo, name: request.Name, sortBy: request.SortBy, sortOrder: request.SortOrder);
            var categoriesCount = await _categoryRepository.CountCategories(request.DateFrom, request.DateTo, name: request.Name);
            int totalPages = (int)Math.Ceiling(categoriesCount * 1.0 / pageSize);

            List<CategoryListResponse> categoryListResponse = _mapper.Map<List<CategoryListResponse>>(categories);

            return new SortedPagedList<CategoryListResponse>
            {
                TotalCount = categoriesCount,
                Items = categoryListResponse,
                Page = page,
                PageSize = pageSize,
                TotalPages = totalPages,
                SortBy = request.SortBy,
                SortOrder = request.SortOrder
            };
        }
    }
}
