using CSharpApp.Application.Abstractions.Queries;
using CSharpApp.Core.Dtos.Products;

namespace CSharpApp.Application.Products.Features.GetProducts;

public class GetProductsQuery : IQuery<IReadOnlyCollection<Product>>
{
}
