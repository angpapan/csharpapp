using CSharpApp.Application.Abstractions.Queries;
using CSharpApp.Core.Dtos.Categories;

namespace CSharpApp.Application.Categories.Features.GetCategoryById;

public class GetCategoryByIdQuery : IQuery<Category>
{
    public int Id { get; }

    public GetCategoryByIdQuery(int id)
    {
        Id = id;
    }
}
