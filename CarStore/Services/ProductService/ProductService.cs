using CarStore.Database.Entities;
using CarStore.DTO.Product.Request;
using CarStore.DTO.Product.Response;
using CarStore.Repository.CategoryRepository;
using CarStore.Repository.ProductRepository;

namespace CarStore.Services.ProductService
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;

        public ProductService(IProductRepository productRepository, ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task<List<ProductResponse>> GetAll()
        {
            List<Product> product = await _productRepository.SelectAllProducts();
            return product.Select(a => new ProductResponse
            {
                Id = a.Id,
                ProductName = a.ProductName,
                Price = a.Price,
                ProductionYear = a.ProductionYear,
                Km = a.Km,
                Description = a.Description,
                Category = new ProductCategoryResponse
                {
                    Category = a.Category.Id,
                    CategoryName = a.Category.CategoryName
                }
            }).ToList();
        }

        public Task<List<Product>> GetAllProductsByCategory(int categoryId)
        {
            return _productRepository.GetProductsByCategory(categoryId);
        }

        public async Task<ProductResponse> GetById(int productId)
        {
            Product product = await _productRepository.SelectProductById(productId);
            return product == null ? null : new ProductResponse
            {
                Id = product.Id,
                ProductName = product.ProductName,
                Price = product.Price,
                ProductionYear = product.ProductionYear,
                Km = product.Km,
                Description = product.Description,
                Category = new ProductCategoryResponse
                {
                    Category = product.Category.Id,
                    CategoryName = product.Category.CategoryName
                }
            };
        }
        public async Task<ProductResponse> Create(NewProduct newProduct)
        {
            Product product = new Product
            {
                ProductName = newProduct.ProductName,
                Price = newProduct.Price,
                ProductionYear = newProduct.ProductionYear,
                Km = newProduct.Km,
                Description = newProduct.Description,
                CategoryId = newProduct.CategoryId
            };

            product = await _productRepository.InsertNewProduct(product);
            await _categoryRepository.SelectCategoryById(product.CategoryId);

            return product == null ? null : new ProductResponse
            {
                Id = product.Id,
                ProductName = product.ProductName,
                Price = product.Price,
                ProductionYear = product.ProductionYear,
                Km = product.Km,
                Description = product.Description,
                Category = new ProductCategoryResponse
                {
                    Category = product.Category.Id,
                    CategoryName = product.Category.CategoryName
                }

            };
        }
        public async Task<ProductResponse> Update(int productId, UpdateProduct updateProduct)
        {
            Product product = new Product
            {
                ProductName = updateProduct.ProductName,
                Price = updateProduct.Price,
                ProductionYear = updateProduct.ProductionYear,
                Km = updateProduct.Km,
                Description = updateProduct.Description,
                CategoryId = updateProduct.CategoryId
            };

            product = await _productRepository.UpdateExistingProduct(productId, product);
            if (product == null) return null;
            else
            {
                await _categoryRepository.SelectCategoryById(product.CategoryId);
                return product == null ? null : new ProductResponse
                {
                    Id = productId,
                    ProductName = product.ProductName,
                    Price = product.Price,
                    ProductionYear = product.ProductionYear,
                    Km = product.Km,
                    Description = product.Description,
                    Category = new ProductCategoryResponse
                    {
                        Category = product.Category.Id,
                        CategoryName = product.Category.CategoryName
                    }
                };
            }
        }
        public async Task<bool> Delete(int productId)
        {
            var result = await _productRepository.DeleteProduct(productId);
            return (result != null);
        }
    }
}
