using CarStore.Database;
using CarStore.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarStore.Repository.CategoryRepository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AbContext _context;


        public CategoryRepository(AbContext context)
        {
            _context = context;
        }

        public async Task<Category> DeleteCategory(int categoryId)
        {
            Category deleteCategory = await _context.Category
                .FirstOrDefaultAsync(category => categoryId == category.Id);

            if (deleteCategory != null)
            {
                _context.Category.Remove(deleteCategory);
                await _context.SaveChangesAsync();
            }
            return deleteCategory;
        }

        public async Task<Category> InsertNewCategory(Category category)
        {
            _context.Category.Add(category);
            await _context.SaveChangesAsync();
            return category;
        }

        public async Task<Category> UpdateExistingCategory(int categoryId, Category category)
        {
            Category updateCategory = await _context.Category
                .FirstOrDefaultAsync(category => category.Id == categoryId);
            if (updateCategory != null)
            {
                updateCategory.CategoryName = category.CategoryName;
                await _context.SaveChangesAsync();
            }
            return updateCategory;
        }

        public async Task<List<Category>> SelectAllCategory()
        {
            return await _context.Category.Include(p => p.Product).ToListAsync();
        }

        public async Task<Category> SelectCategoryById(int categoryId)
        {
            return await _context.Category
            .Include(p => p.Product)
                .FirstOrDefaultAsync(a => a.Id == categoryId);
        }
    }
}
