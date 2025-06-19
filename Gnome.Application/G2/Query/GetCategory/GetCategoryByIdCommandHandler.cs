using AutoMapper;
using Gnome.Domain.Interfaces;
using Gnome.Domain.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Gnome.Application.G2.Query.GetCategory
{
    public class GetCategoryByIdCommandHandler : IRequestHandler<GetCategoryByIdCommand, CategoryResponse>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public GetCategoryByIdCommandHandler(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<CategoryResponse> Handle(GetCategoryByIdCommand request, CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.GetCategoryByIdAsync(request.Id);
            
            if (category == null)
                return null;

            var categoryResponse = _mapper.Map<CategoryResponse>(category);
            return categoryResponse;
        }
    }
} 