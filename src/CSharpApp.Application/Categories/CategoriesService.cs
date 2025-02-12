using CSharpApp.Application.Extensions;
using CSharpApp.Core.Dtos.Categories;
using CSharpApp.Core.Settings.HttpClient;
using CSharpApp.Core.Settings.RestApi;
using System.Net.Http.Json;
using System.Text.Json.Serialization;

namespace CSharpApp.Application.Categories;

public class CategoriesService : ICategoriesService
{
    private readonly HttpClient _httpClient;
    private readonly RestApiSettings _restApiSettings;

    public CategoriesService(
        IOptions<RestApiSettings> restApiSettings,
        IOptions<HttpClientSettings> httpClientSettings,
        IHttpClientFactory httpFactory
        )
    {
        _httpClient = httpFactory.CreateClient(httpClientSettings.Value.Name);
        _restApiSettings = restApiSettings.Value;
    }
    public async Task<Category> CreateCategory(CreateCategory category, CancellationToken cancellationToken = default)
    {
        JsonContent bodyContent = JsonContent.Create(category);

        var response = await _httpClient.PostAsync(_restApiSettings.Categories, bodyContent, cancellationToken);
        var res = await response.ConvertToOrThrow<Category>(cancellationToken);

        return res;
    }

    public async Task<IReadOnlyCollection<Category>> GetCategories(CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.GetAsync(_restApiSettings.Categories, cancellationToken);
        var res = await response.ConvertToOrThrow<List<Category>>(cancellationToken);

        return res.AsReadOnly();
    }

    public async Task<Category> GetCategoryById(int id, CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.GetAsync($"{_restApiSettings.Categories}/{id}", cancellationToken);
        var res = await response.ConvertToOrThrow<Category>(cancellationToken);

        return res;
    }

    public async Task<Category> UpdateCategory(UpdateCategory category, CancellationToken cancellationToken = default)
    {
        JsonContent bodyContent = JsonContent.Create(category, options: new JsonSerializerOptions
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        });

        var response = await _httpClient.PutAsync($"{_restApiSettings.Categories}/{category.Id}", bodyContent, cancellationToken);
        var res = await response.ConvertToOrThrow<Category>(cancellationToken);

        return res;
    }
}
