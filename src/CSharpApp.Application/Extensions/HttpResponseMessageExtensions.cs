namespace CSharpApp.Application.Extensions
{
    internal static class HttpResponseMessageExtensions
    {
        internal static async Task<T> ConvertToOrThrow<T>(this HttpResponseMessage response, CancellationToken cancellationToken = default)
        {
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync(cancellationToken);
            return JsonSerializer.Deserialize<T>(content);
        }
    }
}
