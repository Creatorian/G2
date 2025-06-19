using System;

namespace Gnome.Domain.Exceptions
{
    public class VariantNotFoundException : DomainException
    {
        public int VariantId { get; }

        public VariantNotFoundException(int variantId) 
            : base($"Variant with ID {variantId} was not found.")
        {
            VariantId = variantId;
        }
    }
} 