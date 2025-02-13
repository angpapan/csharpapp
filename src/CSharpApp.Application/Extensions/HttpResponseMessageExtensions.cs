using System.Text.RegularExpressions;

namespace CSharpApp.Application.Extensions
{
    internal static class HttpResponseMessageExtensions
    {
        private static string CleanMessage(string input)
        {
            string result = input.Replace("\"", "");
            result = result.Replace("\n", " ");
            result = Regex.Replace(result, @"\s+", " ");
            result = result.Trim();

            return result;
        }

        internal static async Task<T> ConvertToOrThrow<T>(this HttpResponseMessage response, CancellationToken cancellationToken = default)
        {
            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync(cancellationToken);
                ErrorResponse errorResponse = JsonSerializer.Deserialize<ErrorResponse>(error);
                throw new HttpRequestException(CleanMessage(errorResponse.Message));
            }
            var content = await response.Content.ReadAsStringAsync(cancellationToken);
            return JsonSerializer.Deserialize<T>(content);
        }
    }
}
