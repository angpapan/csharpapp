using CSharpApp.Application.Categories;
using CSharpApp.Core.Dtos.Categories;
using System.Net;
using Tests.Extensions;
using Tests.Fixtures;
using Tests.Mocks;

namespace Tests.ServiceTests;

public class CategoriesServiceTests : IClassFixture<ServiceFixture>
{
    private readonly ServiceFixture _fixture;
    private readonly CategoriesService _categoriesService;

    public CategoriesServiceTests(ServiceFixture fixture)
    {
        _fixture = fixture;
        _categoriesService = new CategoriesService(fixture.RestApiSettings, fixture.HttpClientSettings, new MockHttpClientFactory(fixture.HttpClient));
    }

    [Fact]
    public async Task CreateCategory_ShouldReturn_Category()
    {
        // Arrange
        var createCategoryDto = new CreateCategory("name", "htpps://example.com/image");
        var expectedCategory = new Category
        {
            Id = 1,
            Name = "name",
            Image = "htpps://example.com/image"
        };

        _fixture.HttpMessageHandlerMock.SetupRequest(HttpMethod.Post, HttpStatusCode.OK, expectedCategory);

        // Act
        var result = await _categoriesService.CreateCategory(createCategoryDto);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedCategory.Id, result.Id);
    }

    [Fact]
    public async Task GetCategories_ShouldReturn_Categories()
    {
        // Arrange
        var expectedCategories = new List<Category> {
            new Category
            {
                Id = 1,
                Name = "name",
                Image = "htpps://example.com/image"
            },
            new Category
            {
                Id = 2,
                Name = "name2",
                Image = "htpps://example.com/image"
            }
        };

        _fixture.HttpMessageHandlerMock.SetupRequest(HttpMethod.Get, HttpStatusCode.OK, expectedCategories);

        // Act
        var result = await _categoriesService.GetCategories();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedCategories.Count, result.Count);
        Assert.Equal(result.Count, 2);
        Assert.Equal(expectedCategories[0].Id, result.First().Id);
    }

    [Fact]
    public async Task GetCategoryById_ShouldReturn_Category_WhenCategoryExists()
    {
        // Arrange
        var id = 1;
        var expectedCategory = new Category
        {
            Id = 1,
            Name = "name",
            Image = "htpps://example.com/image"
        };


        _fixture.HttpMessageHandlerMock.SetupRequest(HttpMethod.Get, HttpStatusCode.OK, expectedCategory);

        // Act
        var result = await _categoriesService.GetCategoryById(id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedCategory.Id, id);
    }

    [Fact]
    public async Task GetCategoryById_ShouldThrow_HttpRequestException_WhenCategoryDoesNotExist()
    {
        // Arrange
        var id = 99999;
        var expectedResponse = new
        {
            Name = "EntityNotFoundError"
        };

        _fixture.HttpMessageHandlerMock.SetupRequest(HttpMethod.Get, HttpStatusCode.BadRequest, expectedResponse);

        // Act & Assert
        await Assert.ThrowsAsync<HttpRequestException>(async () => await _categoriesService.GetCategoryById(id));
    }

    [Fact]
    public async Task CreateCategory_ShouldThrow_HttpRequestException_WhenInvalidRequest()
    {
        // Arrange
        var createCategoryDto = new CreateCategory("name", "htpps://example.com/image");
        var expectedResponse = new
        {
            Message = "price must be a positive number"
        };

        _fixture.HttpMessageHandlerMock.SetupRequest(HttpMethod.Post, HttpStatusCode.BadRequest, expectedResponse);

        // Act & Assert
        await Assert.ThrowsAsync<HttpRequestException>(async () => await _categoriesService.CreateCategory(createCategoryDto));
    }

    [Fact]
    public async Task UpdateCategory_ShouldReturn_Category()
    {
        // Arrange
        var updateCategoryDto = new UpdateCategory()
        {
            Id = 1,
            Name = "new_name"
        };
        var expectedCategory = new Category
        {
            Id = 1,
            Name = "new_name",
            Image = "htpps://example.com/image"
        };

        _fixture.HttpMessageHandlerMock.SetupRequest(HttpMethod.Put, HttpStatusCode.OK, expectedCategory);

        // Act
        var result = await _categoriesService.UpdateCategory(updateCategoryDto);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedCategory.Id, result.Id);
        Assert.Equal(expectedCategory.Name, result.Name);
    }
}
