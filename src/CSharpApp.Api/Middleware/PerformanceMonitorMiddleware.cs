using CSharpApp.Core.Settings.PerformanceMonitoring;
using Microsoft.Extensions.Options;
using System.Diagnostics;

namespace CSharpApp.Api.Middleware
{
    public class PerformanceMonitorMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<PerformanceMonitorMiddleware> _logger;
        private readonly int _thresholdInMilliseconds;

        public PerformanceMonitorMiddleware(RequestDelegate next, IOptions<PerformanceMonitoringSettings> performanceSettings, ILogger<PerformanceMonitorMiddleware> logger)
        {
            _next = next;
            _thresholdInMilliseconds = (int)performanceSettings.Value.RequestTimeWarningThresholdInMilliseconds;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var stopwatch = Stopwatch.StartNew();

            await _next(context);

            stopwatch.Stop();

            long elapsedMilliseconds = stopwatch.ElapsedMilliseconds;

            if (elapsedMilliseconds > _thresholdInMilliseconds)
            {
                var requestMethod = context.Request.Method;
                var requestPath = context.Request.Path;
                var statusCode = context.Response.StatusCode;

                _logger.LogWarning("Request {RequestMethod} {RequestPath} responded {StatusCode} in {Elapsed} ms, exceeding the threshold of {Threshold} ms",
                                requestMethod, requestPath, statusCode, elapsedMilliseconds, _thresholdInMilliseconds);
            }
        }
    }
}
