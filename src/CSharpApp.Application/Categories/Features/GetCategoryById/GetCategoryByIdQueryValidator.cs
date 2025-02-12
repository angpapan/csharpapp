using FluentValidation;

namespace CSharpApp.Application.Categories.Features.GetCategoryById;

public class GetCategoryByIdQueryValidator : AbstractValidator<GetCategoryByIdQuery>
{
    public GetCategoryByIdQueryValidator()
    {
        RuleFor(c => c.Id)
            .NotEmpty()
            .WithMessage("Id cannot be empty.")
            .NotNull()
            .WithMessage("Id cannot be null.");
    }
}
