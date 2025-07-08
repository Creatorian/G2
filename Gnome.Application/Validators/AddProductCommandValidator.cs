using FluentValidation;
using Gnome.Application.G2.Query.AddProduct;
using Microsoft.AspNetCore.Http;
using System.Linq;

namespace Gnome.Application.Validators
{
    public class AddProductCommandValidator : AbstractValidator<AddProductCommand>
    {
        public AddProductCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Product name is required")
                .MaximumLength(200).WithMessage("Product name cannot exceed 200 characters")
                .Matches("^[a-zA-Z0-9\\s\\-]+$").WithMessage("Product name can only contain letters, numbers, spaces, and hyphens");

            RuleFor(x => x.Slug)
                .NotEmpty().WithMessage("Product slug is required")
                .MaximumLength(200).WithMessage("Product slug cannot exceed 200 characters")
                .Matches("^[a-z0-9\\-]+$").WithMessage("Product slug can only contain lowercase letters, numbers, and hyphens");

            RuleFor(x => x.Description)
                .MaximumLength(1000).WithMessage("Product description cannot exceed 1000 characters");

            RuleFor(x => x.Price)
                .NotEmpty().WithMessage("Price cannot be empty");

            RuleFor(x => x.Stock)
                .NotEmpty().WithMessage("Stock cannot be empty");

            RuleFor(x => x.CategoryIds)
                    .NotEmpty().WithMessage("At least one category must be selected")
                    .Must(categoryIds => categoryIds.All(id => id > 0)).WithMessage("All category IDs must be greater than 0");
        }
    }
} 