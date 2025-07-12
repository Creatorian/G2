using FluentValidation;
using Gnome.Application.G2.Query.AddCategory;

namespace Gnome.Application.G2.Query.AddCategory
{
    public class AddCategoryCommandValidator : AbstractValidator<AddCategoryCommand>
    {
        public AddCategoryCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Category name is required")
                .MaximumLength(100).WithMessage("Category name cannot exceed 100 characters")
                .Matches("^[a-zA-Z0-9\\s\\-]+$").WithMessage("Category name can only contain letters, numbers, spaces, and hyphens");

            RuleFor(x => x.Slug)
                .NotEmpty().WithMessage("Category slug is required")
                .MaximumLength(100).WithMessage("Category slug cannot exceed 100 characters")
                .Matches("^[a-z0-9\\-]+$").WithMessage("Category slug can only contain lowercase letters, numbers, and hyphens");
        }
    }
} 