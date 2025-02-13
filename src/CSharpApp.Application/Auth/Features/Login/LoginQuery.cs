using CSharpApp.Application.Abstractions.Queries;
using CSharpApp.Core.Dtos.Auth;

namespace CSharpApp.Application.Auth.Features.Login;

public class LoginQuery : IQuery<LoginResponse>
{
    public LoginQuery(string email, string password)
    {
        Email = email;
        Password = password;
    }

    public string Email { get; }
    public string Password { get; }
}
