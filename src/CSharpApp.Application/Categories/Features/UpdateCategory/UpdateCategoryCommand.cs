using CSharpApp.Application.Abstractions.Commands;
using CSharpApp.Core.Dtos.Categories;

namespace CSharpApp.Application.Categories.Features.UpdateCategory;

public class UpdateCategoryCommand : ICommand<Category>
{
    public int Id { get; }

    public string? Name { get; init; } = null;

    public string? Image { get; init; } = null;
    public UpdateCategoryCommand(int id)
    {
        Id = id;
    }
}
