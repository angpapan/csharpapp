namespace CSharpApp.Application.Extensions;

internal static class StringExtensions
{
    internal static bool IsValidUrl(this string str)
    {
        return Uri.TryCreate(str, UriKind.Absolute, out _)
               && (str.StartsWith("http://")
               || str.StartsWith("https://"));
    }
}
