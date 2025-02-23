using Sozeris.Server.Data.Repositories.Interfaces;
using Sozeris.Server.Logic.Services.Interfaces;
using Sozeris.Server.Models.Entities;

namespace Sozeris.Server.Logic.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<IEnumerable<Product>> GetAllProductsAsync()
    {
        var products = await _productRepository.GetAllProductsAsync();
        
        return products;
    }

    public async Task<Product?> GetProductByIdAsync(int productId)
    {
        var product = await _productRepository.GetProductByIdAsync(productId);
        
        return product;
    }

    public async Task<bool> AddProductAsync(Product product)
    {
        return await _productRepository.AddProductAsync(product);
    }

    public async Task<bool> UpdateProductAsync(Product product)
    {
        return await _productRepository.UpdateProductAsync(product);
    }

    public async Task<bool> DeleteProductByIdAsync(int productId)
    {
        return await _productRepository.DeleteProductByIdAsync(productId);
    }
}