using CSharpApp.Core.Settings.HttpClient;
using CSharpApp.Core.Settings.RestApi;
using Microsoft.Extensions.Options;

namespace CSharpApp.Infrastructure.Configuration;

public static class HttpConfiguration
{
    public static IServiceCollection AddHttpConfiguration(this IServiceCollection services)
    {
        var httpSettings = services.BuildServiceProvider().GetRequiredService<IOptions<HttpClientSettings>>().Value;

        services.AddHttpClient(httpSettings.Name, (serviceProvider, client) =>
        {
            var settings = serviceProvider.GetRequiredService<IOptions<RestApiSettings>>().Value;

            client.BaseAddress = new Uri(settings.BaseUrl);
        });

        return services;
    }
}