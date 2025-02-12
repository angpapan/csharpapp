using FluentValidation;

namespace CSharpApp.Application.Categories.Features.UpdateCategory
{
    public class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
    {
        public UpdateCategoryCommandValidator()
        {
            RuleFor(c => c.Id)
                .NotEmpty()
                .WithMessage("Id cannot be empty.")
                .NotNull()
                .WithMessage("Id cannot be null.");

        }
    }
}
