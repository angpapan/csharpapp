using CSharpApp.Application.Abstractions.Commands;
using CSharpApp.Core.Dtos.Products;

namespace CSharpApp.Application.Products.Features.CreateProduct;

internal class CreateProductCommandHandler : ICommandHandler<CreateProductCommand, Product>
{
    IProductsService _productsService;

    public CreateProductCommandHandler(IProductsService productsService)
    {
        _productsService = productsService;
    }

    public async Task<Product> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        Core.Dtos.Products.CreateProduct product = new(
            request.Title,
            request.Price,
            request.Description,
            request.Images,
            request.CategoryId
            );

        return await _productsService.CreateProduct(product, cancellationToken);
    }
}
