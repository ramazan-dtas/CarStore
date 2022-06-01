using CarStore.Database.Entities;

namespace CarStore.Repository.ProductRepository
{
    public interface IProductRepository
    {
        Task<List<Product>> SelectAllProducts();
        Task<List<Product>> GetProductsByCategory(int categoryId);
        Task<Product> SelectProductById(int productId);
        Task<Product> InsertNewProduct(Product product);
        Task<Product> UpdateExistingProduct(int productId, Product product);
        Task<Product> DeleteProduct(int productId);
    }
}
