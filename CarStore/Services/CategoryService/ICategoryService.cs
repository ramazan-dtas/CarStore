using CarStore.DTO.Category.Request;
using CarStore.DTO.Category.Response;

namespace CarStore.Services.CategoryService
{
    public interface ICategoryService
    {
        Task<List<CategoryResponse>> GetAll();
        Task<CategoryResponse> GetById(int CategoryId);
        Task<CategoryResponse> Create(NewCategory newCategory);
        Task<CategoryResponse> Update(int CategoryId, UpdateCategory updateCategory);
        Task<bool> Delete(int CategoryId);
    }
}
