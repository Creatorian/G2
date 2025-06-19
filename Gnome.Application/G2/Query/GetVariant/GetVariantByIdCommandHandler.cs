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

namespace Gnome.Application.G2.Query.GetVariant
{
    public class GetVariantByIdCommandHandler : IRequestHandler<GetVariantByIdCommand, VariantResponse>
    {
        private readonly IVariantRepository _variantRepository;
        private readonly IMapper _mapper;

        public GetVariantByIdCommandHandler(IVariantRepository variantRepository, IMapper mapper)
        {
            _variantRepository = variantRepository;
            _mapper = mapper;
        }

        public async Task<VariantResponse> Handle(GetVariantByIdCommand request, CancellationToken cancellationToken)
        {
            var variant = await _variantRepository.GetVariantByIdAsync(request.Id);
            
            if (variant == null)
                return null;

            var variantResponse = _mapper.Map<VariantResponse>(variant);
            return variantResponse;
        }
    }
} 