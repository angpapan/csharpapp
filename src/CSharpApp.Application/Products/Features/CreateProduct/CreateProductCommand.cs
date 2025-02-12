using CSharpApp.Application.Abstractions.Commands;
using CSharpApp.Core.Dtos.Products;

namespace CSharpApp.Application.Products.Features.CreateProduct;

public class CreateProductCommand : ICommand<Product>
{
    public CreateProductCommand(string title, int price, string description, int categoryId, string[] images)
    {
        Title = title;
        Price = price;
        Description = description;
        CategoryId = categoryId;
        Images = images;
    }

    public string Title { get; }
    public int Price { get; }
    public string Description { get; }
    public string[] Images { get; }
    public int CategoryId { get; }


}
