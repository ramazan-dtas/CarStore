using CarStore.Database.Entities;
using CarStore.DTO.Product.Request;
using CarStore.DTO.Product.Response;

namespace CarStore.Services.ProductService
{
    public interface IProductService
    {
        Task<List<ProductResponse>> GetAll();
        Task<ProductResponse> GetById(int productId);
        Task<ProductResponse> Create(NewProduct newProduct);
        Task<ProductResponse> Update(int productsId, UpdateProduct updateProduct);
        Task<List<Product>> GetAllProductsByCategory(int categoryId);
        Task<bool> Delete(int productId);
    }
}
