using Sozeris.Server.Domain.Entities;
using Sozeris.Server.Logic.Common;

namespace Sozeris.Server.Logic.Interfaces.Services;

public interface IProductService
{
    Task<IReadOnlyList<Product>> GetAllProductsAsync(CancellationToken ct);
    Task<Result<Product>> GetProductByIdAsync(int productId, CancellationToken ct);
    Task<Result<Product>> AddProductAsync(Product product, CancellationToken ct);
    Task<Result<Product>> UpdateProductAsync(int productId, Product product, CancellationToken ct);
    Task<Result> DeleteProductByIdAsync(int productId, CancellationToken ct);
}