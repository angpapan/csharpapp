using CSharpApp.Core.Dtos.Auth;

namespace CSharpApp.Core.Interfaces;

public interface IAuthService
{
    Task<LoginResponse> Login(LoginRequest request, CancellationToken cancellationToken = default);
    Task<Profile> GetProfile(CancellationToken cancellationToken = default);
}
