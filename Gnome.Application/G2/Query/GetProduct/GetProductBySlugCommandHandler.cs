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

namespace Gnome.Application.G2.Query.GetProduct
{
    public class GetProductBySlugCommandHandler : IRequestHandler<GetProductBySlugCommand, ProductResponse>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public GetProductBySlugCommandHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<ProductResponse> Handle(GetProductBySlugCommand request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetProductBySlugAsync(request.Slug);
            
            if (product == null)
                return null;

            var productResponse = _mapper.Map<ProductResponse>(product);
            return productResponse;
        }
    }
} 