using CSharpApp.Application.Abstractions.Queries;
using CSharpApp.Core.Dtos.Categories;

namespace CSharpApp.Application.Categories.Features.GetCategories;

internal class GetCategoriesQueryHandler : IQueryHandler<GetCategoriesQuery, IReadOnlyCollection<Category>>
{
    ICategoriesService _categoriesService;

    public GetCategoriesQueryHandler(ICategoriesService categoriesService)
    {
        _categoriesService = categoriesService;
    }

    public async Task<IReadOnlyCollection<Category>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
    {
        return await _categoriesService.GetCategories(cancellationToken);
    }
}
