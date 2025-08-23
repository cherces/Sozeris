using Sozeris.Server.Domain.Entities;
using Sozeris.Server.Logic.Common;

namespace Sozeris.Server.Logic.Interfaces.Services;

public interface IProductService
{
    Task<IReadOnlyList<Product>> GetAllProductsAsync();
    Task<Result<Product>> GetProductByIdAsync(int productId);
    Task<Result<Product>> AddProductAsync(Product product);
    Task<Result<Product>> UpdateProductAsync(int productId, Product product);
    Task<Result> DeleteProductByIdAsync(int productId);
}