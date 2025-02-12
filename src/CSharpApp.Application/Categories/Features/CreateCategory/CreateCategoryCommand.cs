using CSharpApp.Application.Abstractions.Commands;
using CSharpApp.Core.Dtos.Categories;

namespace CSharpApp.Application.Categories.Features.CreateCategory;

public class CreateCategoryCommand : ICommand<Category>
{
    public CreateCategoryCommand(string name, string image)
    {
        Name = name;
        Image = image;
    }

    public string Name { get; }
    public string Image { get; }
}
