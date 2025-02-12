using Moq;
using Moq.Protected;
using System.Net;
using System.Net.Http.Json;

namespace Tests.Extensions;

public static class HttpMessageHandlerMockExtensions
{
    public static void SetupRequest(
        this Mock<HttpMessageHandler> handlerMock,
        HttpMethod method,
        HttpStatusCode statusCode,
        object expected)
    {
        handlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.Is<HttpRequestMessage>(request => request.Method == method),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = statusCode,
                Content = JsonContent.Create(expected)
            });
    }
}
