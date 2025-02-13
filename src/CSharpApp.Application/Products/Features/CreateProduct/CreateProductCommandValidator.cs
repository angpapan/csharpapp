using CSharpApp.Application.Extensions;
using FluentValidation;

namespace CSharpApp.Application.Products.Features.CreateProduct;

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(p => p.Title)
            .NotEmpty()
            .WithMessage("Title cannot be empty")
            .NotNull()
            .WithMessage("Title cannot be null");

        RuleFor(p => p.Description)
            .NotEmpty()
            .WithMessage("Description cannot be empty")
            .NotNull()
            .WithMessage("Description cannot be null");

        RuleFor(p => p.Price)
            .NotEmpty()
            .WithMessage("Price cannot be empty")
            .NotNull()
            .WithMessage("Price cannot be null")
            .GreaterThan(0)
            .WithMessage("Price must be a positive number");

        RuleFor(p => p.CategoryId)
            .NotEmpty()
            .WithMessage("CategoryId cannot be empty")
            .NotNull()
            .WithMessage("CategoryId cannot be null");

        RuleFor(x => x.Images)
            .NotNull()
            .WithMessage("Images cannot be empty")
            .NotEmpty()
            .WithMessage("The images must contain at least one element.")
            .Must(urls => urls.All(url => url.IsValidUrl()))
            .WithMessage("Images should only contain url addresses.");
    }
}
