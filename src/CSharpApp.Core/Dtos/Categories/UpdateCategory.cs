﻿namespace CSharpApp.Core.Dtos.Categories;

public class UpdateCategory
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; } = null;

    [JsonPropertyName("image")]
    public string? Image { get; set; } = null;
}
