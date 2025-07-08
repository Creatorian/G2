using FluentValidation;
using Gnome.Application.G2.Query.UpdateProduct;
using Microsoft.AspNetCore.Http;
using System.Linq;

namespace Gnome.Application.Validators
{
    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Product ID must be greater than 0");

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

            RuleFor(x => x.ShortDescription)
                .MaximumLength(500).WithMessage("Product short description cannot exceed 500 characters");

            RuleFor(x => x.Price)
                .NotEmpty().WithMessage("Price cannot be empty");

            RuleFor(x => x.Stock)
                .NotEmpty().WithMessage("Stock cannot be empty");

            RuleFor(x => x.CategoryIds)
                .Must(categoryIds => categoryIds == null || categoryIds.All(id => id > 0))
                .WithMessage("All category IDs must be greater than 0");

            RuleFor(x => x.Complexity)
                .MaximumLength(50).WithMessage("Complexity cannot exceed 50 characters");

            RuleFor(x => x.NumberOfPlayers)
                .MaximumLength(50).WithMessage("Number of players cannot exceed 50 characters");

            RuleFor(x => x.PlayingTime)
                .MaximumLength(50).WithMessage("Playing time cannot exceed 50 characters");

            RuleFor(x => x.CommunityAge)
                .MaximumLength(50).WithMessage("Community age cannot exceed 50 characters");

            RuleFor(x => x.Awards)
                .Must(awards => awards == null || awards.All(award => !string.IsNullOrEmpty(award) && award.Length <= 100))
                .WithMessage("Each award must not be empty and cannot exceed 100 characters");
        }
    }
} 