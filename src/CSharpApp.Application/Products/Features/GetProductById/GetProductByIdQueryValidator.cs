using FluentValidation;

namespace CSharpApp.Application.Products.Features.GetProductById;

public class GetProductByIdQueryValidator : AbstractValidator<GetProductByIdQuery>
{
    public GetProductByIdQueryValidator()
    {
        RuleFor(c => c.Id)
            .NotNull()
            .WithMessage($"The Id cannot be null.")
            .NotEmpty()
            .WithMessage($"The Id cannot be empty.");
    }
}
