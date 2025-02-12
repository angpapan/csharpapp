using CSharpApp.Core.Settings.HttpClient;
using CSharpApp.Core.Settings.RestApi;

namespace CSharpApp.Infrastructure.Configuration;

public static class DefaultConfiguration
{
    public static IServiceCollection AddDefaultConfiguration(this IServiceCollection services)
    {
        services.ConfigureOptions<HttpClientSettingsSetup>();
        services.ConfigureOptions<RestApiSettingsSetup>();

        services.AddSingleton<IProductsService, ProductsService>();

        return services;
    }
}