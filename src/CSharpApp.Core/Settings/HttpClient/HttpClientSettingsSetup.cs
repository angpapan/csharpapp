using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace CSharpApp.Core.Settings.HttpClient;

public class HttpClientSettingsSetup : IConfigureOptions<HttpClientSettings>
{
    private const string SectionName = "HttpClientSettings";

    private readonly IConfiguration _configuration;

    public HttpClientSettingsSetup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void Configure(HttpClientSettings options)
    {
        _configuration
            .GetSection(SectionName)
            .Bind(options);
    }
}
