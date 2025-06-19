using AutoMapper;
using Gnome.Domain.Common;
using Gnome.Domain.Interfaces;
using Gnome.Domain.Responses;
using Gnome.Infrastructure.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Gnome.Application.G2.Query.ListVariants
{
    public class ListVariantsQueryCommandHandler : IRequestHandler<ListVariantsQueryCommand, SortedPagedList<VariantListResponse>>
    {
        private readonly IVariantRepository _variantRepository;
        private readonly IMapper _mapper;

        public ListVariantsQueryCommandHandler(IVariantRepository variantRepository, IMapper mapper)
        {
            _variantRepository = variantRepository;
            _mapper = mapper;
        }

        public async Task<SortedPagedList<VariantListResponse>> Handle(ListVariantsQueryCommand request, CancellationToken cancellationToken)
        {
            var page = request.Page;
            var pageSize = request.PageSize;
            var variants = await _variantRepository.GetVariants(page, pageSize, request.DateFrom, request.DateTo, name: request.Name, sortBy: request.SortBy, sortOrder: request.SortOrder);
            var variantsCount = await _variantRepository.CountVariants(request.DateFrom, request.DateTo, name: request.Name);
            int totalPages = (int)Math.Ceiling(variantsCount * 1.0 / pageSize);

            List<VariantListResponse> variantListResponse = _mapper.Map<List<VariantListResponse>>(variants);

            return new SortedPagedList<VariantListResponse>
            {
                TotalCount = variantsCount,
                Items = variantListResponse,
                Page = page,
                PageSize = pageSize,
                TotalPages = totalPages,
                SortBy = request.SortBy,
                SortOrder = request.SortOrder
            };
        }
    }
}
