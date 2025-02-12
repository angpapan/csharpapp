using CSharpApp.Application.Categories.Features.CreateCategory;
using CSharpApp.Application.Categories.Features.GetCategories;
using CSharpApp.Application.Categories.Features.GetCategoryById;
using CSharpApp.Application.Categories.Features.UpdateCategory;
using CSharpApp.Core.Dtos.Categories;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CSharpApp.Api.Endpoints;

public static class CategoriesEndpoints
{
    public static void MapCategoriesEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("api/v{version:apiVersion}/getcategories", async (ISender sender, CancellationToken cancellationToken = default) =>
        {
            GetCategoriesQuery query = new GetCategoriesQuery();
            var categories = await sender.Send(query, cancellationToken);
            return categories;
        })
        .WithName("GetCategories")
        .HasApiVersion(1.0);

        app.MapGet("api/v{version:apiVersion}/categories/{id}", async (
            ISender sender,
            [FromRoute] int id,
            CancellationToken cancellationToken = default
            ) =>
        {
            GetCategoryByIdQuery query = new GetCategoryByIdQuery(id);
            var category = await sender.Send(query, cancellationToken);
            return category;
        })
        .WithName("GetCategoryById")
        .HasApiVersion(1.0);

        app.MapPost("api/v{version:apiVersion}/categories", async (
            ISender sender,
            [FromBody] CreateCategory request,
            CancellationToken cancellationToken = default
            ) =>
        {
            CreateCategoryCommand command = new CreateCategoryCommand(
                request.Name,
                request.Image
                );

            var category = await sender.Send(command, cancellationToken);
            return category;
        })
        .WithName("CreateCategory")
        .HasApiVersion(1.0);

        app.MapPut("api/v{version:apiVersion}/categories/{id}", async (
            ISender sender,
            int id,
            [FromBody] UpdateCategory request,
            CancellationToken cancellationToken = default
            ) =>
        {
            UpdateCategoryCommand command = new UpdateCategoryCommand(id)
            {
                Name = request.Name,
                Image = request.Image
            };

            var category = await sender.Send(command, cancellationToken);
            return category;
        })
        .WithName("UpdateCategory")
        .HasApiVersion(1.0);
    }
}
