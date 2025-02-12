using CSharpApp.Core.Dtos.Products;

namespace CSharpApp.Core.Interfaces;

public interface IProductsService
{
    Task<IReadOnlyCollection<Product>> GetProducts(CancellationToken cancellationToken = default);
    Task<Product> GetProductById(int id, CancellationToken cancellationToken = default);
    Task<Product> CreateProduct(CreateProduct product, CancellationToken cancellationToken = default);
}