using CSharpApp.Application.Abstractions.Commands;
using CSharpApp.Core.Dtos.Products;

namespace CSharpApp.Application.Products.Features.GetProductById;

public class GetProductByIdQuery : ICommand<Product>
{
    public int Id { get; }

    public GetProductByIdQuery(int id)
    {
        Id = id;
    }
}
