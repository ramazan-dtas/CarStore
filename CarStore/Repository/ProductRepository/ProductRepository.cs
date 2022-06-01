using CarStore.Database;
using CarStore.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarStore.Repository.ProductRepository
{
    public class ProductRepository : IProductRepository
    {
        private readonly AbContext _context;

        public ProductRepository(AbContext context)
        {
            _context = context;
        }

        public async Task<Product> DeleteProduct(int productId)
        {
            Product deleteProduct = await _context.Product
                .FirstOrDefaultAsync(product => product.Id == productId);

            if (deleteProduct != null)
            {
                _context.Product.Remove(deleteProduct);
                await _context.SaveChangesAsync();
            }
            return deleteProduct;
        }

        public async Task<Product> InsertNewProduct(Product product)
        {
            _context.Product.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<List<Product>> SelectAllProducts()
        {
            return await _context.Product.Include(p => p.Category).ToListAsync();
        }

        public async Task<List<Product>> GetProductsByCategory(int categoryId)
        {
            return await _context.Product.Where(a => a.CategoryId == categoryId).Include(p => p.Category).ToListAsync();
        }

        public async Task<Product> SelectProductById(int productId)
        {
            return await _context.Product
                .Include(p => p.Category)
                .FirstOrDefaultAsync(a => a.Id == productId);
        }

        public async Task<Product> UpdateExistingProduct(int productId, Product product)
        {
            Product updateProduct = await _context.Product
                .FirstOrDefaultAsync(product => product.Id == productId);
            if (updateProduct != null)
            {
                updateProduct.ProductName = product.ProductName;
                updateProduct.Price = product.Price;
                updateProduct.ProductionYear = product.ProductionYear;
                updateProduct.Description = product.Description;
                updateProduct.Km = product.Km;
                await _context.SaveChangesAsync();
            }
            return updateProduct;
        }
    }
}
