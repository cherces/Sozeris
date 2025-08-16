using Sozeris.Server.Domain.Entities;
using Sozeris.Server.Domain.Interfaces.Repositories;
using Sozeris.Server.Domain.Interfaces.Services;

namespace Sozeris.Server.Logic.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<IReadOnlyList<Product>> GetAllProductsAsync()
    {
        var products = await _productRepository.GetAllProductsAsync();
        
        return products;
    }

    public async Task<Product?> GetProductByIdAsync(int productId)
    {
        var product = await _productRepository.GetProductByIdAsync(productId);
        
        return product;
    }

    public async Task<Product?> AddProductAsync(Product product)
    {
        var created = await _productRepository.AddProductAsync(product);
        
        return created;
    }

    public async Task<bool> UpdateProductAsync(int productId, Product product)
    {
        var existing = await _productRepository.GetProductByIdAsync(productId);
        if (existing is null)
            return false;

        existing.CloneFrom(product);
        return await _productRepository.UpdateProductAsync(existing);
    }

    public async Task<bool> DeleteProductByIdAsync(int productId)
    {
        return await _productRepository.DeleteProductByIdAsync(productId);
    }
}