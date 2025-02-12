namespace CSharpApp.Core.Dtos.Categories;

public class CreateCategory
{
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("image")]
    public string? Image { get; set; }
}
