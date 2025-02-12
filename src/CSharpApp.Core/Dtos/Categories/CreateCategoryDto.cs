namespace CSharpApp.Core.Dtos.Categories;

public class CreateCategory
{
    public CreateCategory(string? name, string? image)
    {
        Name = name;
        Image = image;
    }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("image")]
    public string Image { get; set; }
}
