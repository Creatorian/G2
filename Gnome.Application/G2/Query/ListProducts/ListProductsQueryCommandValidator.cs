using FluentValidation;
using Gnome.Application.G2.Query.ListProducts;

namespace Gnome.Application.G2.Query.ListProducts
{
    public class ListProductsQueryCommandValidator : AbstractValidator<ListProductsQueryCommand>
    {
        public ListProductsQueryCommandValidator()
        {
            // Only validate pagination properties, not filter properties
            RuleFor(x => x.Page)
                .GreaterThan(0).WithMessage("Page must be greater than 0");

            RuleFor(x => x.PageSize)
                .GreaterThan(0).WithMessage("Page size must be greater than 0")
                .LessThanOrEqualTo(100).WithMessage("Page size cannot exceed 100");
        }
    }
} 