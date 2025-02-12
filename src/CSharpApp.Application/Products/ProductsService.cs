using CSharpApp.Core.Settings.HttpClient;
using CSharpApp.Core.Settings.RestApi;

namespace CSharpApp.Application.Products;

public class ProductsService : IProductsService
{
    private readonly HttpClient _httpClient;
    private readonly RestApiSettings _restApiSettings;
    private readonly ILogger<ProductsService> _logger;

    public ProductsService(
        IOptions<RestApiSettings> restApiSettings,
        IOptions<HttpClientSettings> httpClientSettings,
        ILogger<ProductsService> logger,
        IHttpClientFactory httpFactory
        )
    {
        _httpClient = httpFactory.CreateClient(httpClientSettings.Value.Name);
        _restApiSettings = restApiSettings.Value;
        _logger = logger;
    }

    public async Task<IReadOnlyCollection<Product>> GetProducts()
    {
        var response = await _httpClient.GetAsync(_restApiSettings.Products);
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        var res = JsonSerializer.Deserialize<List<Product>>(content);

        return res.AsReadOnly();
    }
}