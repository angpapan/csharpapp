using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace CSharpApp.Core.Settings.PerformanceMonitoring;

public class PerformanceMonitoringSettingsSetup : IConfigureOptions<PerformanceMonitoringSettings>
{
    private const string SectionName = "PerformanceMonitoringSettings";

    private readonly IConfiguration _configuration;

    public PerformanceMonitoringSettingsSetup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void Configure(PerformanceMonitoringSettings options)
    {
        _configuration
            .GetSection(SectionName)
            .Bind(options);
    }
}
