using CSharpApp.Core.Settings.HttpClient;
using CSharpApp.Core.Settings.RestApi;
using Microsoft.Extensions.Options;
using Moq;

namespace Tests.Fixtures;

public class ServiceFixture : IDisposable
{
    public Mock<HttpMessageHandler> HttpMessageHandlerMock { get; }
    public HttpClient HttpClient { get; }
    public IOptions<RestApiSettings> RestApiSettings { get; }
    public IOptions<HttpClientSettings> HttpClientSettings { get; }

    public ServiceFixture()
    {
        HttpMessageHandlerMock = new Mock<HttpMessageHandler>();
        HttpClient = new HttpClient(HttpMessageHandlerMock.Object);
        RestApiSettings = Options.Create(new RestApiSettings
        {
            Products = "https://api.example.com/products",
            Categories = "https://api.example.com/categories",
            Auth = "https://api.example.com/auth/login",
            Profile = "https://api.example.com/auth/profile",
            Username = "john@mail.com",
            Password = "changeme",
            AuthTokenCacheId = "auth-token",
            AuthTokenExpirationInMinutes = 5
        });

        HttpClientSettings = Options.Create(new HttpClientSettings
        {
            Name = "TestClient"
        });
    }

    public void Dispose()
    {
        HttpClient.Dispose();
    }
}
