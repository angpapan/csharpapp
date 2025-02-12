using CSharpApp.Application.Abstractions.Queries;
using CSharpApp.Core.Dtos.Categories;

namespace CSharpApp.Application.Categories.Features.GetCategoryById;

internal class GetCategoryByIdQueryHandler : IQueryHandler<GetCategoryByIdQuery, Category>
{
    ICategoriesService _categoriesService;
    public GetCategoryByIdQueryHandler(ICategoriesService categoriesService)
    {
        _categoriesService = categoriesService;
    }

    public async Task<Category> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        return await _categoriesService.GetCategoryById(request.Id, cancellationToken);
    }
}
