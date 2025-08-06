using FluentValidation;
using Gnome.Application.G2.Query.AddProduct;
using Microsoft.AspNetCore.Http;
using System.Linq;

namespace Gnome.Application.G2.Query.AddProduct
{
    public class AddProductCommandValidator : AbstractValidator<AddProductCommand>
    {
        public AddProductCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name must be at least 3 characters long.")
                .MinimumLength(3).WithMessage("Name must be at least 3 characters long.")
                .MaximumLength(200).WithMessage("Product name cannot exceed 200 characters")
                .Matches("^[a-zA-Z0-9\\s\\-]+$").WithMessage("Product name can only contain letters, numbers, spaces, and dashes");

            RuleFor(x => x.Slug)
                .NotEmpty().WithMessage("Slug must be at least 3 characters long.")
                .MinimumLength(3).WithMessage("Slug must be at least 3 characters long.")
                .MaximumLength(200).WithMessage("Product slug cannot exceed 200 characters")
                .Matches("^[a-z0-9]+(-[a-z0-9]+)*$").WithMessage("Slug must be lowercase and dash-separated.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description must be at least 8 characters long.")
                .MinimumLength(8).WithMessage("Description must be at least 8 characters long.")
                .MaximumLength(2000).WithMessage("Product description cannot exceed 2000 characters");

            RuleFor(x => x.ShortDescription)
                .NotEmpty().WithMessage("Short description must be at least 8 characters long.")
                .MinimumLength(8).WithMessage("Short description must be at least 8 characters long.")
                .MaximumLength(500).WithMessage("Product short description cannot exceed 500 characters");

            RuleFor(x => x.NumberOfPlayers)
                .NotEmpty().WithMessage("Must be two numbers separated by a dash (e.g., 2-4).")
                .Matches("^\\d+\\s*-\\s*\\d+$").WithMessage("Must be two numbers separated by a dash (e.g., 2-4).")
                .MaximumLength(20).WithMessage("Number of players cannot exceed 20 characters");

            RuleFor(x => x.PlayingTime)
                .NotEmpty().WithMessage("Must be two numbers separated by a dash (e.g., 30-60).")
                .Matches("^\\d+\\s*-\\s*\\d+$").WithMessage("Must be two numbers separated by a dash (e.g., 30-60).")
                .MaximumLength(20).WithMessage("Playing time cannot exceed 20 characters");

            RuleFor(x => x.CommunityAge)
                .NotEmpty().WithMessage("Community age must be a number.")
                .GreaterThan(0).WithMessage("Community age must be greater than 0.");

            RuleFor(x => x.Rating)
                .NotEmpty().WithMessage("Rating must be a number.")
                .GreaterThanOrEqualTo(0).WithMessage("Rating must be 0 or greater.")
                .LessThanOrEqualTo(5).WithMessage("Rating must be 5 or less.");

            RuleFor(x => x.Complexity)
                .NotEmpty().WithMessage("Complexity must be a number.")
                .GreaterThanOrEqualTo(1).WithMessage("Complexity must be 1 or greater.")
                .LessThanOrEqualTo(5).WithMessage("Complexity must be 5 or less.");

            RuleFor(x => x.Price)
                .NotEmpty().WithMessage("Price must be a number.")
                .GreaterThanOrEqualTo(1).WithMessage("Price must be at least 1.");

            RuleFor(x => x.Stock)
                .NotEmpty().WithMessage("Stock must be a number.")
                .GreaterThanOrEqualTo(0).WithMessage("Stock must be 0 or greater.");

            RuleFor(x => x.Awards)
                .Must(awards => awards == null || awards.All(award => !string.IsNullOrEmpty(award) && award.Length <= 100))
                .WithMessage("Each award must not be empty and cannot exceed 100 characters");

            RuleFor(x => x.CategoryIds)
                .NotEmpty().WithMessage("At least one category must be selected")
                .Must(categoryIds => categoryIds.All(id => id > 0)).WithMessage("All category IDs must be greater than 0");
        }
    }
} 