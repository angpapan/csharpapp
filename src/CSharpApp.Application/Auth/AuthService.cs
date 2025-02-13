using CSharpApp.Application.Extensions;
using CSharpApp.Core.Dtos.Auth;
using CSharpApp.Core.Settings.HttpClient;
using CSharpApp.Core.Settings.RestApi;
using Microsoft.Extensions.Caching.Memory;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace CSharpApp.Application.Auth;

public class AuthService : IAuthService
{
    private readonly HttpClient _httpClient;
    private readonly RestApiSettings _restApiSettings;
    private readonly IMemoryCache _cache;

    public AuthService(
        IOptions<RestApiSettings> restApiSettings,
        IOptions<HttpClientSettings> httpClientSettings,
        IHttpClientFactory httpFactory,
        IMemoryCache cache
        )
    {
        _httpClient = httpFactory.CreateClient(httpClientSettings.Value.Name);
        _restApiSettings = restApiSettings.Value;
        _cache = cache;
    }

    public async Task<LoginResponse> Login(LoginRequest request, CancellationToken cancellationToken = default)
    {
        JsonContent bodyContent = JsonContent.Create(request);

        var response = await _httpClient.PostAsync(_restApiSettings.Auth, bodyContent, cancellationToken);
        var res = await response.ConvertToOrThrow<LoginResponse>(cancellationToken);

        // add to cache
        string token = res.AccessToken;
        var expiration = DateTimeOffset.UtcNow.AddMinutes((double)_restApiSettings.AuthTokenExpirationInMinutes);
        _cache.Set(_restApiSettings.AuthTokenCacheId, token, expiration);

        return res;
    }

    public async Task<Profile> GetProfile(CancellationToken cancellationToken = default)
    {
        if (!_cache.TryGetValue(_restApiSettings.AuthTokenCacheId, out string token))
        {
            token = string.Empty;
        }

        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var response = await _httpClient.GetAsync($"{_restApiSettings.Auth}/profile", cancellationToken);
        var res = await response.ConvertToOrThrow<Profile>(cancellationToken);

        return res;
    }

}
