using FluentValidation;

namespace CSharpApp.Application.Products.Features.CreateProduct;

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(p => p.Title)
            .NotEmpty()
            .NotNull()
            .Configure(rule => rule.MessageBuilder = _ => "Title cannot be empty");

        RuleFor(p => p.Description)
            .NotEmpty()
            .NotNull()
            .Configure(rule => rule.MessageBuilder = _ => "Description cannot be empty");

        RuleFor(p => p.Price)
            .NotEmpty()
            .WithMessage("Price cannot be empty")
            .NotNull()
            .WithMessage("Price cannot be null")
            .GreaterThan(0)
            .WithMessage("Price must be a positive number");

        RuleFor(p => p.CategoryId)
            .NotEmpty()
            .NotNull()
            .Configure(rule => rule.MessageBuilder = _ => "CategoryId cannot be empty");

        RuleFor(x => x.Images)
            .NotNull()
            .WithMessage("Images cannot be empty")
            .NotEmpty()
            .WithMessage("The images must contain at least one element.")
            .Must(urls => urls.All(IsValidUrl))
            .WithMessage("Images should only contain url addresses.");
    }

    private bool IsValidUrl(string url)
    {
        return Uri.TryCreate(url, UriKind.Absolute, out _)
               && (url.StartsWith("http://")
               || url.StartsWith("https://"));
    }
}
