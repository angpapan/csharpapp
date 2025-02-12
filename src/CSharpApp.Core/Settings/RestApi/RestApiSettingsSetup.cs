using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace CSharpApp.Core.Settings.RestApi;

public class RestApiSettingsSetup : IConfigureOptions<RestApiSettings>
{
    private const string SectionName = "RestApiSettings";

    private readonly IConfiguration _configuration;

    public RestApiSettingsSetup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void Configure(RestApiSettings options)
    {
        _configuration
            .GetSection(SectionName)
            .Bind(options);
    }
}