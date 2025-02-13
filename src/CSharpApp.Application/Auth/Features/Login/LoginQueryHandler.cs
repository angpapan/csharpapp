using CSharpApp.Application.Abstractions.Queries;
using CSharpApp.Core.Dtos.Auth;

namespace CSharpApp.Application.Auth.Features.Login
{
    internal class LoginQueryHandler : IQueryHandler<LoginQuery, LoginResponse>
    {
        IAuthService _authService;

        public LoginQueryHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<LoginResponse> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            LoginRequest loginRequest = new()
            {
                Email = request.Email,
                Password = request.Password,
            };

            return await _authService.Login(loginRequest, cancellationToken);
        }
    }
}
