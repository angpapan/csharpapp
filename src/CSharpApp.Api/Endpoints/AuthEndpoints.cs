using CSharpApp.Application.Auth.Features.GetProfile;
using CSharpApp.Application.Auth.Features.Login;
using CSharpApp.Core.Dtos.Auth;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CSharpApp.Api.Endpoints;

public static class AuthEndpoints
{
    public static void MapAuthEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("api/v{version:apiVersion}/auth/login", async (
            ISender sender,
            [FromBody] LoginRequest request,
            CancellationToken cancellationToken = default
            ) =>
        {
            LoginQuery query = new LoginQuery(request.Email, request.Password);
            var tokens = await sender.Send(query, cancellationToken);
            return tokens;
        })
        .WithName("Login")
        .HasApiVersion(1.0);

        app.MapGet("api/v{version:apiVersion}/auth/profile", async (
            ISender sender,
            CancellationToken cancellationToken = default
            ) =>
        {
            GetProfileQuery query = new GetProfileQuery();
            var profile = await sender.Send(query, cancellationToken);
            return profile;
        })
        .WithName("GetProfile")
        .HasApiVersion(1.0);
    }
}
