using CSharpApp.Core.Settings.HttpClient;
using Microsoft.Extensions.Options;
using Polly;
using Polly.Extensions.Http;

namespace CSharpApp.Infrastructure.Configuration
{
    internal static class HttpClientPolicies
    {
        internal static IAsyncPolicy<HttpResponseMessage> RetryPolicy(IOptions<HttpClientSettings> settings)
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .WaitAndRetryAsync(
                    retryCount: settings.Value.RetryCount,
                    sleepDurationProvider: retryAttempt => TimeSpan.FromMilliseconds(retryAttempt * settings.Value.SleepDuration)
                    );
        }
    }
}
