using CSharpApp.Application.Products;
using CSharpApp.Core.Dtos.Products;
using System.Net;
using Tests.Extensions;
using Tests.Fixtures;
using Tests.Mocks;

namespace Tests.ServiceTests;

public class ProductsServiceTests : IClassFixture<ServiceFixture>
{
    private readonly ServiceFixture _fixture;
    private readonly ProductsService _productsService;

    public ProductsServiceTests(ServiceFixture fixture)
    {
        _fixture = fixture;
        _productsService = new ProductsService(fixture.RestApiSettings, fixture.HttpClientSettings, new MockHttpClientFactory(fixture.HttpClient));
    }

    [Fact]
    public async Task CreateProduct_ShouldReturn_Product()
    {
        // Arrange
        var createProductDto = new CreateProduct("title", 1, "description", ["https://example.com"], 1);
        var expectedProduct = new Product
        {
            Id = 1,
            Title = "title",
            Price = 1,
            Description = "description",
        };

        _fixture.HttpMessageHandlerMock.SetupRequest(HttpMethod.Post, HttpStatusCode.OK, expectedProduct);

        // Act
        var result = await _productsService.CreateProduct(createProductDto);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedProduct.Id, result.Id);
    }

    [Fact]
    public async Task GetProducts_ShouldReturn_Products()
    {
        // Arrange
        var expectedProducts = new List<Product> {
            new Product
            {
                Id = 1,
                Title = "title",
                Price = 1,
                Description = "description",
            },
            new Product
            {
                Id = 2,
                Title = "title2",
                Price = 1,
                Description = "description2",
            }
        };

        _fixture.HttpMessageHandlerMock.SetupRequest(HttpMethod.Get, HttpStatusCode.OK, expectedProducts);

        // Act
        var result = await _productsService.GetProducts();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedProducts.Count, result.Count);
        Assert.Equal(result.Count, 2);
        Assert.Equal(expectedProducts[0].Id, result.First().Id);
    }

    [Fact]
    public async Task GetProductById_ShouldReturn_Product_WhenProductExists()
    {
        // Arrange
        var id = 1;
        var expectedProduct =
            new Product
            {
                Id = 1,
                Title = "title",
                Price = 1,
                Description = "description",
            };


        _fixture.HttpMessageHandlerMock.SetupRequest(HttpMethod.Get, HttpStatusCode.OK, expectedProduct);

        // Act
        var result = await _productsService.GetProductById(id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedProduct.Id, id);
    }

    [Fact]
    public async Task GetProductById_ShouldThrow_HttpRequestException_WhenProductDoesNotExist()
    {
        // Arrange
        var id = 99999;
        var expectedResponse = new
        {
            Name = "EntityNotFoundError"
        };

        _fixture.HttpMessageHandlerMock.SetupRequest(HttpMethod.Get, HttpStatusCode.BadRequest, expectedResponse);

        // Act & Assert
        await Assert.ThrowsAsync<HttpRequestException>(async () => await _productsService.GetProductById(id));
    }

    [Fact]
    public async Task CreateProduct_ShouldThrow_HttpRequestException_WhenInvalidRequest()
    {
        // Arrange
        var createProductDto = new CreateProduct("title", -1, "description", ["https://example.com"], 1);
        var expectedResponse = new
        {
            Message = "price must be a positive number"
        };

        _fixture.HttpMessageHandlerMock.SetupRequest(HttpMethod.Post, HttpStatusCode.BadRequest, expectedResponse);

        // Act & Assert
        await Assert.ThrowsAsync<HttpRequestException>(async () => await _productsService.CreateProduct(createProductDto));
    }
}
