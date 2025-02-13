using CSharpApp.Application.Auth;
using CSharpApp.Core.Dtos.Auth;
using Microsoft.Extensions.Caching.Memory;
using Moq;
using System.Net;
using Tests.Extensions;
using Tests.Fixtures;
using Tests.Mocks;

namespace Tests.ServiceTests;

public class AuthServiceTests : IClassFixture<ServiceFixture>
{
    private readonly ServiceFixture _fixture;
    private readonly AuthService _authService;
    private readonly Mock<IMemoryCache> _cacheMock;

    public AuthServiceTests(ServiceFixture fixture)
    {
        _fixture = fixture;
        _cacheMock = new Mock<IMemoryCache>();
        _authService = new AuthService(fixture.RestApiSettings, fixture.HttpClientSettings, new MockHttpClientFactory(fixture.HttpClient), _cacheMock.Object);
    }

    [Fact]
    public async Task Login_ShouldReturn_LoginResponse_WhenSuccessful()
    {
        // Arrange
        var loginRequest = new LoginRequest()
        {
            Email = _fixture.RestApiSettings.Value.Username,
            Password = _fixture.RestApiSettings.Value.Password
        };

        var expectedResponse = new LoginResponse
        {
            AccessToken = "access.token",
            RefreshToken = "refresh.token"
        };

        _fixture.HttpMessageHandlerMock.SetupRequest(HttpMethod.Post, HttpStatusCode.OK, expectedResponse);

        // Mock IMemoryCache to prevent NullReferenceException
        var cacheEntryMock = new Mock<ICacheEntry>();
        cacheEntryMock.SetupProperty(e => e.Value);

        _cacheMock
            .Setup(c => c.CreateEntry(It.IsAny<object>()))
            .Returns(cacheEntryMock.Object);

        // Act
        var result = await _authService.Login(loginRequest);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedResponse.AccessToken, result.AccessToken);
    }

    [Fact]
    public async Task Login_ShouldCache_AccessToken_WhenSuccessful()
    {
        // Arrange
        var loginRequest = new LoginRequest()
        {
            Email = _fixture.RestApiSettings.Value.Username,
            Password = _fixture.RestApiSettings.Value.Password
        };

        var expectedResponse = new LoginResponse
        {
            AccessToken = "access.token",
            RefreshToken = "refresh.token"
        };

        _fixture.HttpMessageHandlerMock.SetupRequest(HttpMethod.Post, HttpStatusCode.OK, expectedResponse);

        var expectedToken = expectedResponse.AccessToken;
        object actualStoredValue = null;

        var cacheEntryMock = new Mock<ICacheEntry>();
        cacheEntryMock.SetupProperty(e => e.Value);
        cacheEntryMock
            .SetupSet(e => e.Value = It.IsAny<object>())
            .Callback<object>(v => actualStoredValue = v);

        _cacheMock
            .Setup(c => c.CreateEntry(It.IsAny<object>()))
            .Returns(cacheEntryMock.Object);

        // Act
        var result = await _authService.Login(loginRequest);

        // Assert
        _cacheMock.Verify(
                c => c.CreateEntry(_fixture.RestApiSettings.Value.AuthTokenCacheId),
                Times.Once
            );
        Assert.Equal("access.token", actualStoredValue);
    }

    [Fact]
    public async Task Login_ShouldThrow_HttpRequestException_WhenInvalidCredentials()
    {
        // Arrange
        var loginRequest = new LoginRequest()
        {
            Email = "test@test.com",
            Password = "1234"
        };

        var expectedResponse = new
        {
            Message = "Unauthorized",
            StatusCode = 401
        };

        _fixture.HttpMessageHandlerMock.SetupRequest(HttpMethod.Post, HttpStatusCode.BadRequest, expectedResponse);

        // Mock IMemoryCache to prevent NullReferenceException
        var cacheEntryMock = new Mock<ICacheEntry>();
        cacheEntryMock.SetupProperty(e => e.Value);

        _cacheMock
            .Setup(c => c.CreateEntry(It.IsAny<object>()))
            .Returns(cacheEntryMock.Object);

        // Act & Assert
        await Assert.ThrowsAsync<HttpRequestException>(async () => await _authService.Login(loginRequest));
    }

    [Fact]
    public async Task GetProfile_ShouldReturn_Profile_WhenAuthenticated()
    {
        // Arrange
        var expectedResponse = new Profile
        {
            Id = 1,
            Name = "John"
        };

        var cachedToken = "access.token";

        // Mock IMemoryCache to prevent NullReferenceException
        var cacheEntryMock = new Mock<ICacheEntry>();
        cacheEntryMock.SetupProperty(e => e.Value);
        cacheEntryMock
            .SetupSet(e => e.Value = It.IsAny<object>())
            .Callback<object>(v => v = cachedToken);

        _cacheMock
            .Setup(c => c.CreateEntry(It.IsAny<object>()))
            .Returns(cacheEntryMock.Object);


        _fixture.HttpMessageHandlerMock.SetupRequest(HttpMethod.Get, HttpStatusCode.OK, expectedResponse);

        // Act
        var result = await _authService.GetProfile();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedResponse.Id, result.Id);
        Assert.Equal(expectedResponse.Name, result.Name);
    }
}
