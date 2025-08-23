using Sozeris.Server.Domain.Entities;

namespace Sozeris.Server.Domain.Interfaces.Repositories;

public interface IProductRepository
{
    Task<IReadOnlyList<Product>> GetAllProductsAsync();
    Task<Product?> GetProductByIdAsync(int productId);
    Task<Product> AddProductAsync(Product product);
    Task<Product> UpdateProductAsync(Product product);
    Task DeleteProductByIdAsync(Product product);
}