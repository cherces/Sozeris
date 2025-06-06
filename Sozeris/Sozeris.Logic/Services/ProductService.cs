using Sozeris.Logic.Services.Interfaces;
using Sozeris.Models.Entities;

namespace Sozeris.Logic.Services;

public class ProductService : IProductService
{
    public async Task<IEnumerable<Product>> GetAllProductsAsync()
    {
        await Task.Delay(500);

        return new List<Product>
        {
            new Product { Id = 1, Name = "Белый премиум", Price = 30, Image = "bread_white_2.png" },
            new Product { Id = 2, Name = "Черный премиум", Price = 35, Image = "bread_dark_2.png" },
            new Product { Id = 3, Name = "Белый простой", Price = 20, Image = "bread_white_1.png" },
            new Product { Id = 4, Name = "Черный простой", Price = 25, Image = "bread_dark_1.png" }
        };
    }
}