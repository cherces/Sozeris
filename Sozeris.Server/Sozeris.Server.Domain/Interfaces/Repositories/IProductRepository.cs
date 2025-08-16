using Sozeris.Server.Domain.Entities;

namespace Sozeris.Server.Domain.Interfaces.Repositories;

public interface IProductRepository
{
    Task<IReadOnlyList<Product>> GetAllProductsAsync();
    Task<Product?> GetProductByIdAsync(int productId);
    Task<Product?> AddProductAsync(Product product);
    Task<bool> UpdateProductAsync(Product product);
    Task<bool> DeleteProductByIdAsync(int productId);
}