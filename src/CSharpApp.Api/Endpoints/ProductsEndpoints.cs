using CSharpApp.Application.Products.Features.CreateProduct;
using CSharpApp.Application.Products.Features.GetProductById;
using CSharpApp.Application.Products.Features.GetProducts;
using CSharpApp.Core.Dtos.Products;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CSharpApp.Api.Endpoints;

public static class ProductsEndpoints
{
    public static void MapProductsEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("api/v{version:apiVersion}/getproducts", async (ISender sender, CancellationToken cancellationToken = default) =>
        {
            GetProductsQuery query = new GetProductsQuery();
            var products = await sender.Send(query, cancellationToken);
            return products;
        })
        .WithName("GetProducts")
        .HasApiVersion(1.0);

        app.MapGet("api/v{version:apiVersion}/products/{id}", async (
            ISender sender,
            [FromRoute] int id,
            CancellationToken cancellationToken = default
            ) =>
        {
            GetProductByIdQuery query = new GetProductByIdQuery(id);
            var products = await sender.Send(query, cancellationToken);
            return products;
        })
        .WithName("GetProductById")
        .HasApiVersion(1.0);

        app.MapPost("api/v{version:apiVersion}/products", async (
            ISender sender,
            [FromBody] CreateProduct request,
            CancellationToken cancellationToken = default
            ) =>
        {
            CreateProductCommand command = new CreateProductCommand(
                request.Title,
                request.Price,
                request.Description,
                request.CategoryId,
                request.Images
                );

            var products = await sender.Send(command, cancellationToken);
            return products;
        })
        .WithName("CreateProduct")
        .HasApiVersion(1.0);
    }
}
