using Sozeris.Models.Entities;

namespace Sozeris.Logic.Services.Interfaces;

public interface IProductService
{
    Task<IEnumerable<Product>> GetAllProductsAsync();
}