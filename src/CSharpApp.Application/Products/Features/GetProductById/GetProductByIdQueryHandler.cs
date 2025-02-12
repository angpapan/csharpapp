using CSharpApp.Application.Abstractions.Commands;
using CSharpApp.Core.Dtos.Products;

namespace CSharpApp.Application.Products.Features.GetProductById;

internal class GetProductByIdQueryHandler : ICommandHandler<GetProductByIdQuery, Product>
{
    IProductsService _productsService;

    public GetProductByIdQueryHandler(IProductsService productsService)
    {
        _productsService = productsService;
    }

    public async Task<Product> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        return await _productsService.GetProductById(request.Id, cancellationToken);
    }
}
