using FluentValidation;
using Gnome.Application.G2.Query.AddCategory;

namespace Gnome.Application.G2.Query.AddCategory
{
    public class AddCategoryCommandValidator : AbstractValidator<AddCategoryCommand>
    {
        public AddCategoryCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name must be at least 3 characters long.")
                .MinimumLength(3).WithMessage("Name must be at least 3 characters long.")
                .MaximumLength(100).WithMessage("Category name cannot exceed 100 characters")
                .Matches("^[a-zA-Z0-9\\s\\-]+$").WithMessage("Category name can only contain letters, numbers, spaces, and dashes");

            RuleFor(x => x.Slug)
                .NotEmpty().WithMessage("Slug must be at least 3 characters long.")
                .MinimumLength(3).WithMessage("Slug must be at least 3 characters long.")
                .MaximumLength(100).WithMessage("Category slug cannot exceed 100 characters")
                .Matches("^[a-z0-9]+(-[a-z0-9]+)*$").WithMessage("Slug must be lowercase and dash-separated.");
        }
    }
} 