using CSharpApp.Application.Extensions;
using FluentValidation;

namespace CSharpApp.Application.Categories.Features.CreateCategory
{
    public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
    {
        public CreateCategoryCommandValidator()
        {
            RuleFor(c => c.Name)
                .NotEmpty()
                .WithMessage("Name cannot be empty.")
                .NotNull()
                .WithMessage("Name cannot be null.");

            RuleFor(c => c.Image)
                .NotEmpty()
                .WithMessage("Image cannot be empty.")
                .NotNull()
                .WithMessage("Image cannot be null")
                .Must(image => image.IsValidUrl())
                .When(c => !string.IsNullOrEmpty(c.Image))
                .WithMessage("The image must be a valid URL.");
        }
    }
}
