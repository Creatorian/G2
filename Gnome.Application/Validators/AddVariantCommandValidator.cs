using FluentValidation;
using Gnome.Application.G2.Query.AddVariant;

namespace Gnome.Application.Validators
{
    public class AddVariantCommandValidator : AbstractValidator<AddVariantCommand>
    {
        public AddVariantCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Variant name is required")
                .MaximumLength(200).WithMessage("Variant name cannot exceed 200 characters")
                .Matches("^[a-zA-Z0-9\\s\\-]+$").WithMessage("Variant name can only contain letters, numbers, spaces, and hyphens");

            RuleFor(x => x.Slug)
                .NotEmpty().WithMessage("Variant slug is required")
                .MaximumLength(200).WithMessage("Variant slug cannot exceed 200 characters")
                .Matches("^[a-z0-9\\-]+$").WithMessage("Variant slug can only contain lowercase letters, numbers, and hyphens");

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("Price must be greater than 0")
                .LessThan(1000000).WithMessage("Price cannot exceed 1,000,000");

            RuleFor(x => x.Stock)
                .GreaterThanOrEqualTo(0).WithMessage("Stock cannot be negative")
                .LessThan(1000000).WithMessage("Stock cannot exceed 1,000,000");

            RuleFor(x => x.ProductId)
                .GreaterThan(0).WithMessage("Product ID must be greater than 0");
        }
    }
} 