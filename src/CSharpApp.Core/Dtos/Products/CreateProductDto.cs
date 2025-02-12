namespace CSharpApp.Core.Dtos.Products;

public class CreateProduct
{
    public CreateProduct(string title, int price, string description, string[] images, int categoryId)
    {
        Title = title;
        Price = price;
        Description = description;
        Images = images;
        CategoryId = categoryId;
    }

    [JsonPropertyName("title")]
    public string Title { get; init; }

    [JsonPropertyName("price")]
    public int Price { get; init; }

    [JsonPropertyName("description")]
    public string Description { get; init; }

    [JsonPropertyName("images")]
    public string[] Images { get; init; } = [];

    [JsonPropertyName("categoryId")]
    public int CategoryId { get; init; }
}
