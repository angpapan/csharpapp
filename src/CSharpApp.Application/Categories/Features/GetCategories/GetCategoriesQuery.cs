using CSharpApp.Application.Abstractions.Queries;
using CSharpApp.Core.Dtos.Categories;

namespace CSharpApp.Application.Categories.Features.GetCategories;

public class GetCategoriesQuery : IQuery<IReadOnlyCollection<Category>>
{
}
