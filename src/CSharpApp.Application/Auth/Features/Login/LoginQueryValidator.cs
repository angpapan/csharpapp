using FluentValidation;

namespace CSharpApp.Application.Auth.Features.Login;

public class LoginQueryValidator : AbstractValidator<LoginQuery>
{
    public LoginQueryValidator()
    {
        RuleFor(q => q.Password)
            .NotEmpty()
            .WithMessage("Password cannot be empty.")
            .NotNull()
            .WithMessage("Password cannot be null.");

        RuleFor(q => q.Email)
            .NotEmpty()
            .WithMessage("Email cannot be empty.")
            .NotNull()
            .WithMessage("Email cannot be null.")
            .EmailAddress()
            .WithMessage("Email should be a valid mail address");
    }
}
