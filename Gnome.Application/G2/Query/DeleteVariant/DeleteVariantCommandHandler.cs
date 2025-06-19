using Gnome.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Gnome.Application.G2.Query.DeleteVariant
{
    public class DeleteVariantCommandHandler : IRequestHandler<DeleteVariantCommand, bool>
    {
        private readonly IVariantRepository _variantRepository;

        public DeleteVariantCommandHandler(IVariantRepository variantRepository)
        {
            _variantRepository = variantRepository;
        }

        public async Task<bool> Handle(DeleteVariantCommand request, CancellationToken cancellationToken)
        {
            var result = await _variantRepository.DeleteVariantAsync(request.Id);
            return result;
        }
    }
} 