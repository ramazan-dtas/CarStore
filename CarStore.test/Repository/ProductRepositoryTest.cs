using CarStore.Database;
using CarStore.Database.Entities;
using CarStore.Repository.ProductRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarStore.test.Repository
{
    public class ProductRepositoryTest
    {
        private readonly DbContextOptions<AbContext> _options;
        private readonly AbContext _context;
        private readonly ProductRepository _productRepository;

        public ProductRepositoryTest()
        {
            _options = new DbContextOptionsBuilder<AbContext>()
                .UseInMemoryDatabase(databaseName: "CarStoreproduct")
                .Options;

            _context = new(_options);
            _productRepository = new(_context);
        }

        [Fact]
        public async void SelectAllProduct_ShouldReturnListOfProducts_WhenProductsExists()
        {
            //Arrange
            await _context.Database.EnsureDeletedAsync();

            _context.Product.Add(new()
            {
                Id = 1,
                ProductName = "db11",
                Price = 5330000,
                ProductionYear = 2020,
                Km = 100,
                Description = "Den har meget sej",
                Category = new()
            });
            _context.Product.Add(new()
            {
                Id = 2,
                ProductName = "Ghost",
                Price = 1919000,
                ProductionYear = 2020,
                Km = 268,
                Description = "Den har meget flot",
                Category = new()
            });

            await _context.SaveChangesAsync();
            //act
            var result = await _productRepository.SelectAllProducts();

            //assert
            Assert.NotNull(result);
            Assert.IsType<List<Product>>(result);
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async void SelectAllProducts_ShouldReturnEmptyListOfProducts_WhenNoProductsExists()
        {
            //Arrange
            await _context.Database.EnsureDeletedAsync();

            //Act
            var result = await _productRepository.SelectAllProducts();

            //Assert
            Assert.NotNull(result);
            Assert.IsType<List<Product>>(result);
            Assert.Empty(result);
        }

        [Fact]
        public async void SelectProductById_ShouldReturnProduct_WhenProductExists()
        {
            //Arrange
            await _context.Database.EnsureDeletedAsync();

            int productId = 1;

            _context.Product.Add(new()
            {
                Id = productId,
                ProductName = "Db11",
                Price = 100,
                ProductionYear = 2020,
                Km = 1000,
                Description = "Den har meget flot",
                Category = new()
            });

            await _context.SaveChangesAsync();

            //Act
            var result = await _productRepository.SelectProductById(productId);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<Product>(result);
            Assert.Equal(productId, result.Id);
        }

        [Fact]
        public async void SelectProductById_ShouldReturnNull_WhenProductDoesNotExist()
        {
            //Arrange
            await _context.Database.EnsureDeletedAsync();

            //Act
            var result = await _productRepository.SelectProductById(1);

            //Assert
            Assert.Null(result);
            //Assert
        }

        [Fact]
        public async void InsertNewProduct_ShouldAddNewIdToProduct_WhenSavingToDatabase()
        {
            //Arrange
            await _context.Database.EnsureDeletedAsync();

            int expectedNewId = 1;

            Product product = new()
            {
                ProductName = "Db11",
                Price = 100,
                ProductionYear = 2020,
                Km = 1000,
                Description = "Den har meget flot"
            };

            //Act
            var result = await _productRepository.InsertNewProduct(product);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<Product>(result);
            Assert.Equal(expectedNewId, result.Id);
        }

        [Fact]
        public async void InsertNewProduct_ShouldFailToAddNewProduct_WhenProductIdAlreadyExists()
        {
            //Arrange
            await _context.Database.EnsureDeletedAsync();

            Product product = new()
            {
                Id = 1,
                ProductName = "Db11",
                Price = 100,
                ProductionYear = 2020,
                Km = 1000,
                Description = "Den har meget flot"
            };

            _context.Product.Add(product);
            await _context.SaveChangesAsync();

            //Act
            async Task action() => await _productRepository.InsertNewProduct(product);

            //Assert
            var ex = await Assert.ThrowsAsync<ArgumentException>(action);
            Assert.Contains("An item with the same key has already been added", ex.Message);
        }

        [Fact]
        public async void UpdateExistingProduct_ShouldChangeValuesOnProduct_WhenProductExists()
        {
            //Arrange
            await _context.Database.EnsureDeletedAsync();

            int productId = 1;

            Product newProduct = new()
            {
                Id = productId,
                ProductName = "Db11",
                Price = 100,
                ProductionYear = 2020,
                Km = 1000,
                Description = "Den har meget flot"
            };

            _context.Product.Add(newProduct);
            await _context.SaveChangesAsync();

            Product updateProduct = new()
            {
                Id = productId,
                ProductName = "Db11",
                Price = 100,
                ProductionYear = 2020,
                Km = 1000,
                Description = "Den har meget flot"
            };

            //Act
            var result = await _productRepository.UpdateExistingProduct(productId, updateProduct);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<Product>(result);
            Assert.Equal(productId, result.Id);
            Assert.Equal(updateProduct.ProductName, result.ProductName);
        }

        [Fact]
        public async void UpdateExistingProduct_ShouldReturnNull_WhenProductDoesNotExist()
        {
            //Arrange
            await _context.Database.EnsureDeletedAsync();

            int productId = 1;

            Product updateProduct = new()
            {
                Id = productId,
                ProductName = "Db11",
                Price = 100,
                ProductionYear = 2020,
                Km = 1000,
                Description = "Den har meget flot"
            };

            //Act
            var result = await _productRepository.UpdateExistingProduct(productId, updateProduct);

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async void DeletedProductById_ShouldReturnDeletedProduct_WhenProductIsDeleted()
        {
            //Arrange
            await _context.Database.EnsureDeletedAsync();

            int productId = 1;

            Product newProduct = new()
            {
                ProductName = "Db11",
                Price = 100,
                ProductionYear = 2020,
                Km = 1000,
                Description = "Den har meget flot"
            };

            _context.Product.Add(newProduct);
            await _context.SaveChangesAsync();

            //Act
            var result = await _productRepository.DeleteProduct(productId);
            var product = await _productRepository.SelectProductById(productId);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<Product>(result);
            Assert.Equal(productId, result.Id);
            Assert.Null(product);
        }

        [Fact]
        public async void DeleteProductById_ShouldReturnNull_WhenProductDoesNotExist()
        {
            //Arrange
            await _context.Database.EnsureDeletedAsync();

            //Act
            var result = await _productRepository.DeleteProduct(1);

            //Assert
            Assert.Null(result);
        }
    }
}
