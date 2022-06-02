using CarStore.Database;
using CarStore.Database.Entities;
using CarStore.Repository.CategoryRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarStore.test.Repository
{
    public class CategoryRepositoryTest
    {
        private readonly DbContextOptions<AbContext> _options;
        private readonly AbContext _context;
        private readonly CategoryRepository _categoryRepository;

        public CategoryRepositoryTest()
        {
            _options = new DbContextOptionsBuilder<AbContext>()
                .UseInMemoryDatabase(databaseName: "CarStoreCategory")
                .Options;

            _context = new(_options);
            _categoryRepository = new(_context);
        }

        [Fact]
        public async void SelectAllCategory_ShouldReturnListOfCategorys_WhenCategorysExists()
        {
            //Arrange
            await _context.Database.EnsureDeletedAsync();

            _context.Category.Add(new()
            {
                Id = 1,
                CategoryName = "Aston Martin",
            });
            _context.Category.Add(new()
            {
                Id = 2,
                CategoryName = "Rolls-Royce",

            });

            await _context.SaveChangesAsync();
            //act
            var result = await _categoryRepository.SelectAllCategory();

            //assert
            Assert.NotNull(result);
            Assert.IsType<List<Category>>(result);
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async void SelectAllCategorys_ShouldReturnEmptyListOfCategorys_WhenNoCategorysExists()
        {
            //Arrange
            await _context.Database.EnsureDeletedAsync();

            //Act
            var result = await _categoryRepository.SelectAllCategory();

            //Assert
            Assert.NotNull(result);
            Assert.IsType<List<Category>>(result);
            Assert.Empty(result);
        }

        [Fact]
        public async void SelectCategoryById_ShouldReturnCategory_WhenCategoryExists()
        {
            //Arrange
            await _context.Database.EnsureDeletedAsync();

            int categoryId = 1;

            _context.Category.Add(new()
            {
                Id = categoryId,
                CategoryName = "Aston Martin"
            });

            await _context.SaveChangesAsync();

            //Act
            var result = await _categoryRepository.SelectCategoryById(categoryId);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<Category>(result);
            Assert.Equal(categoryId, result.Id);
        }

        [Fact]
        public async void SelectCategoryById_ShouldReturnNull_WhenCategoryDoesNotExist()
        {
            //Arrange
            await _context.Database.EnsureDeletedAsync();

            //Act
            var result = await _categoryRepository.SelectCategoryById(1);

            //Assert
            Assert.Null(result);
            //Assert
        }

        [Fact]
        public async void InsertNewCategory_ShouldAddNewIdToCategory_WhenSavingToDatabase()
        {
            //Arrange
            await _context.Database.EnsureDeletedAsync();

            int expectedNewId = 1;

            Category category = new()
            {
                CategoryName = "Aston Martin"
            };

            //Act
            var result = await _categoryRepository.InsertNewCategory(category);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<Category>(result);
            Assert.Equal(expectedNewId, result.Id);
        }

        [Fact]
        public async void InsertNewCategory_ShouldFailToAddNewCategory_WhenCategoryIdAlreadyExists()
        {
            //Arrange
            await _context.Database.EnsureDeletedAsync();

            Category category = new()
            {
                Id = 1,
                CategoryName = "Aston Martin"
            };

            _context.Category.Add(category);
            await _context.SaveChangesAsync();

            //Act
            async Task action() => await _categoryRepository.InsertNewCategory(category);

            //Assert
            var ex = await Assert.ThrowsAsync<ArgumentException>(action);
            Assert.Contains("An item with the same key has already been added", ex.Message);
        }

        [Fact]
        public async void UpdateExistingCategory_ShouldChangeValuesOnCategory_WhenCategoryExists()
        {
            //Arrange
            await _context.Database.EnsureDeletedAsync();

            int categoryId = 1;

            Category newCategory = new()
            {
                Id = categoryId,
                CategoryName = "Aston Martin"
            };

            _context.Category.Add(newCategory);
            await _context.SaveChangesAsync();

            Category updateCategory = new()
            {
                Id = categoryId,
                CategoryName = "updated Aston Martin"
            };

            //Act
            var result = await _categoryRepository.UpdateExistingCategory(categoryId, updateCategory);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<Category>(result);
            Assert.Equal(categoryId, result.Id);
            Assert.Equal(updateCategory.CategoryName, result.CategoryName);
        }

        [Fact]
        public async void UpdateExistingCategory_ShouldReturnNull_WhenCategoryDoesNotExist()
        {
            //Arrange
            await _context.Database.EnsureDeletedAsync();

            int categoryId = 1;

            Category updateCategory = new()
            {
                Id = categoryId,
                CategoryName = "Aston Martin"
            };

            //Act
            var result = await _categoryRepository.UpdateExistingCategory(categoryId, updateCategory);

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async void DeleteCategoryById_ShouldReturnDeletedCategory_WhenCategoryIsDeleted()
        {
            //Arrange
            await _context.Database.EnsureDeletedAsync();

            int categoryId = 1;

            Category newCategory = new()
            {
                CategoryName = "Aston Martin"
            };

            _context.Category.Add(newCategory);
            await _context.SaveChangesAsync();

            //Act
            var result = await _categoryRepository.DeleteCategory(categoryId);
            var category = await _categoryRepository.SelectCategoryById(categoryId);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<Category>(result);
            Assert.Equal(categoryId, result.Id);
            Assert.Null(category);
        }

        [Fact]
        public async void DeleteCategoryById_ShouldReturnNull_WhenCategoryDoesNotExist()
        {
            //Arrange
            await _context.Database.EnsureDeletedAsync();

            //Act
            var result = await _categoryRepository.DeleteCategory(1);

            //Assert
            Assert.Null(result);
        }
    }
}
