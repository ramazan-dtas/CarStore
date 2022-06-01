using CarStore.Database;
using CarStore.Database.Entities;
using CarStore.DTO.Category.Request;
using CarStore.DTO.Category.Response;
using CarStore.Repository.CategoryRepository;

namespace CarStore.Services.CategoryService
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<List<CategoryResponse>> GetAll()
        {
            List<Category> Category = await _categoryRepository.SelectAllCategory();
            return Category.Select(c => new CategoryResponse
            {
                Id = c.Id,
                CategoryName = c.CategoryName,
                Products = c.Product.Select(p => new CategoryProductResponse
                {
                    ProductID = p.Id,
                    ProductName = p.ProductName,
                    Price = p.Price,
                    ProductionYear = p.ProductionYear,
                    Km = p.Km,
                    Description = p.Description

                }).ToList()

            }).ToList();
        }
        public async Task<CategoryResponse> GetById(int CategoryId)
        {
            Category Category = await _categoryRepository.SelectCategoryById(CategoryId);
            return Category == null ? null : new CategoryResponse
            {
                Id = Category.Id,
                CategoryName = Category.CategoryName,
                Products = Category.Product.Select(p => new CategoryProductResponse
                {
                    ProductID = p.Id,
                    ProductName = p.ProductName,
                    Price = p.Price,
                    ProductionYear = p.ProductionYear,
                    Km = p.Km,
                    Description = p.Description
                }).ToList()
            };
        }
        public async Task<CategoryResponse> Create(NewCategory newCategory)
        {
            Category category = new Category
            {
                CategoryName = newCategory.categoryName
            };

            category = await _categoryRepository.InsertNewCategory(category);

            return category == null ? null : new CategoryResponse
            {
                Id = category.Id,
                CategoryName = category.CategoryName
            };
        }
        public async Task<CategoryResponse> Update(int CategoryId, UpdateCategory updateCategory)
        {
            Category Category = new Category
            {
                CategoryName = updateCategory.categoryName
            };

            Category = await _categoryRepository.UpdateExistingCategory(CategoryId, Category);

            return Category == null ? null : new CategoryResponse
            {
                Id = Category.Id,
                CategoryName = Category.CategoryName
            };
        }
        public async Task<bool> Delete(int CategoryId)
        {
            var result = await _categoryRepository.DeleteCategory(CategoryId);
            if (result != null) return true;
            else return false;
        }
    }
}
