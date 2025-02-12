using CSharpApp.Application.Abstractions.Queries;
using CSharpApp.Core.Dtos.Products;

namespace CSharpApp.Application.Products.Features.GetProducts;

internal class GetProductsQueryHandler : IQueryHandler<GetProductsQuery, IReadOnlyCollection<Product>>
{
    IProductsService _productsService;

    public GetProductsQueryHandler(IProductsService productsService)
    {
        _productsService = productsService;
    }

    public async Task<IReadOnlyCollection<Product>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        return await _productsService.GetProducts(cancellationToken);
    }
}
