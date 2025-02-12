using CSharpApp.Application.Abstractions.Commands;
using CSharpApp.Core.Dtos.Categories;

namespace CSharpApp.Application.Categories.Features.UpdateCategory;

internal class UpdateCategoryCommandHandler : ICommandHandler<UpdateCategoryCommand, Category>
{
    ICategoriesService _categoriesService;

    public UpdateCategoryCommandHandler(ICategoriesService categoriesService)
    {
        _categoriesService = categoriesService;
    }
    public async Task<Category> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        Core.Dtos.Categories.UpdateCategory category = new()
        {
            Id = request.Id,
            Name = request.Name,
            Image = request.Image
        };

        return await _categoriesService.UpdateCategory(category);
    }
}
