namespace CSharpApp.Core.Dtos.Auth;

public class LoginRequest
{
    [JsonPropertyName("email")]
    public string Email { get; set; }
    [JsonPropertyName("password")]
    public string Password { get; set; }
}
