using Sozeris.Server.Domain.Entities;

namespace Sozeris.Server.Domain.Interfaces.Repositories;

public interface IProductRepository
{
    Task<IReadOnlyList<Product>> GetAllProductsAsync(CancellationToken ct);
    Task<Product?> GetProductByIdAsync(int productId, CancellationToken ct);
    Task<Product> AddProductAsync(Product product, CancellationToken ct);
    Task<Product> UpdateProductAsync(Product product, CancellationToken ct);
    Task DeleteProductByIdAsync(Product product, CancellationToken ct);
}