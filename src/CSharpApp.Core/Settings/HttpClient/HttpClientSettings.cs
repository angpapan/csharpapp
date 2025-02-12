namespace CSharpApp.Core.Settings.HttpClient;

public sealed class HttpClientSettings
{
    public string Name { get; set; }
    public int LifeTime { get; set; }
    public int RetryCount { get; set; }
    public int SleepDuration { get; set; }
}