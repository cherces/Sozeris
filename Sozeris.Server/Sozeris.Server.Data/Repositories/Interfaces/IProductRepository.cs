using Sozeris.Server.Models.Entities;

namespace Sozeris.Server.Data.Repositories.Interfaces;

public interface IProductRepository
{
    public Task<IEnumerable<Product>> GetProductsAsync();
    public Task<Product> GetProductByIdAsync(int id);
    public Task<bool> AddProductAsync(Product product);
    public Task<bool> UpdateProductAsync(Product product);
    public Task<bool> DeleteProductAsync(int id);
}