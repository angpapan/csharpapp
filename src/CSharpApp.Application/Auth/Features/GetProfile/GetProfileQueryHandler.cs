using CSharpApp.Application.Abstractions.Queries;
using CSharpApp.Core.Dtos.Auth;

namespace CSharpApp.Application.Auth.Features.GetProfile;

internal class GetProfileQueryHandler : IQueryHandler<GetProfileQuery, Profile>
{
    IAuthService _authService;

    public GetProfileQueryHandler(IAuthService authService)
    {
        _authService = authService;
    }

    public async Task<Profile> Handle(GetProfileQuery request, CancellationToken cancellationToken)
    {
        return await _authService.GetProfile(cancellationToken);
    }
}
