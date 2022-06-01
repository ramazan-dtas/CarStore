using CarStore.Database.Entities;

namespace CarStore.Repository.CategoryRepository
{
    public interface ICategoryRepository
    {
        Task<List<Category>> SelectAllCategory();
        Task<Category> SelectCategoryById(int categoryId);
        Task<Category> InsertNewCategory(Category category);
        Task<Category> UpdateExistingCategory(int categoryId, Category category);
        Task<Category> DeleteCategory(int categoryId);
    }
}
