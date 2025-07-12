using FluentValidation;
using Gnome.Application.G2.Query.ListCategories;

namespace Gnome.Application.G2.Query.ListCategories
{
    public class ListCategoriesQueryCommandValidator : AbstractValidator<ListCategoriesQueryCommand>
    {
        public ListCategoriesQueryCommandValidator()
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