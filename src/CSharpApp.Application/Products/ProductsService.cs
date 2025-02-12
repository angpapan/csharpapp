using CSharpApp.Core.Dtos.Products;
using CSharpApp.Core.Settings.HttpClient;
using CSharpApp.Core.Settings.RestApi;
using System.Net.Http.Json;

namespace CSharpApp.Application.Products;

public class ProductsService : IProductsService
{
    private readonly HttpClient _httpClient;
    private readonly RestApiSettings _restApiSettings;

    public ProductsService(
        IOptions<RestApiSettings> restApiSettings,
        IOptions<HttpClientSettings> httpClientSettings,
        IHttpClientFactory httpFactory
        )
    {
        _httpClient = httpFactory.CreateClient(httpClientSettings.Value.Name);
        _restApiSettings = restApiSettings.Value;
    }

    public async Task<Product> CreateProduct(CreateProduct product, CancellationToken cancellationToken = default)
    {
        JsonContent bodyContent = JsonContent.Create(product);

        var response = await _httpClient.PostAsync(_restApiSettings.Products, bodyContent, cancellationToken);
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        var res = JsonSerializer.Deserialize<Product>(content);

        return res;
    }

    public async Task<Product> GetProductById(int id, CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.GetAsync($"{_restApiSettings.Products}/{id}", cancellationToken);
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        var res = JsonSerializer.Deserialize<Product>(content);

        return res;
    }

    public async Task<IReadOnlyCollection<Product>> GetProducts(CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.GetAsync(_restApiSettings.Products, cancellationToken);
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        var res = JsonSerializer.Deserialize<List<Product>>(content);

        return res.AsReadOnly();
    }
}