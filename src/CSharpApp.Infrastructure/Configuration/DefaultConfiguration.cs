using CSharpApp.Application.Behaviours;
using CSharpApp.Application.Categories;
using CSharpApp.Core.Settings.HttpClient;
using CSharpApp.Core.Settings.RestApi;
using FluentValidation;
using System.Reflection;

namespace CSharpApp.Infrastructure.Configuration;

public static class DefaultConfiguration
{
    public static IServiceCollection AddDefaultConfiguration(this IServiceCollection services)
    {
        services.ConfigureOptions<HttpClientSettingsSetup>();
        services.ConfigureOptions<RestApiSettingsSetup>();

        Assembly applicationAssembly = Assembly.Load("CSharpApp.Application");
        services.AddValidatorsFromAssembly(applicationAssembly);

        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(applicationAssembly);

            config.AddOpenBehavior(typeof(ValidationBehaviour<,>));
        });

        services.AddCors(options =>
        {
            options.AddDefaultPolicy(
                policy =>
                {
                    policy
                        .WithOrigins("*")
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
        });

        services.AddSingleton<IProductsService, ProductsService>();
        services.AddSingleton<ICategoriesService, CategoriesService>();

        return services;
    }
}