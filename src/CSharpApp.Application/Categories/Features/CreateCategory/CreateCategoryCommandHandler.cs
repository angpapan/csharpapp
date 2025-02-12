using CSharpApp.Application.Abstractions.Commands;
using CSharpApp.Core.Dtos.Categories;

namespace CSharpApp.Application.Categories.Features.CreateCategory;

internal class CreateCategoryCommandHandler : ICommandHandler<CreateCategoryCommand, Category>
{
    ICategoriesService _categoriesService;

    public CreateCategoryCommandHandler(ICategoriesService categoriesService)
    {
        _categoriesService = categoriesService;
    }

    public async Task<Category> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        Core.Dtos.Categories.CreateCategory category = new(request.Name, request.Image);

        return await _categoriesService.CreateCategory(category, cancellationToken);
    }
}
