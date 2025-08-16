using Sozeris.Server.Domain.Entities;

namespace Sozeris.Server.Domain.Interfaces.Services;

public interface IProductService
{
    Task<IReadOnlyList<Product>> GetAllProductsAsync();
    Task<Product?> GetProductByIdAsync(int productId);
    Task<Product?> AddProductAsync(Product product);
    Task<bool> UpdateProductAsync(int productId, Product product);
    Task<bool> DeleteProductByIdAsync(int productId);
}