using CSharpApp.Core.Dtos.Categories;

namespace CSharpApp.Core.Interfaces;

public interface ICategoriesService
{
    public Task<IReadOnlyCollection<Category>> GetCategories(CancellationToken cancellationToken = default);
    public Task<Category> GetCategoryById(int id, CancellationToken cancellationToken = default);
    public Task<Category> CreateCategory(CreateCategory category, CancellationToken cancellationToken = default);
    public Task<Category> UpdateCategory(UpdateCategory category, CancellationToken cancellationToken = default);
}
