using Sozeris.Server.Domain.Entities;
using Sozeris.Server.Domain.Interfaces.Repositories;
using Sozeris.Server.Logic.Common;
using Sozeris.Server.Logic.Interfaces.Services;

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

    public async Task<Result<Product>> GetProductByIdAsync(int productId)
    {
        var result = await _productRepository.GetProductByIdAsync(productId);
        if (result == null) return Result<Product>.Fail("Product not found"); 
        
        return Result<Product>.Ok(result);
    }

    public async Task<Result<Product>> AddProductAsync(Product product)
    {
        var newProduct = await _productRepository.AddProductAsync(product);
        
        return Result<Product>.Ok(newProduct);
    }

    public async Task<Result<Product>> UpdateProductAsync(int productId, Product product)
    {
        var existing = await _productRepository.GetProductByIdAsync(productId);
        if (existing is null) return Result<Product>.Fail("Product not found");

        existing.CloneFrom(product);
        var updateProduct = await _productRepository.UpdateProductAsync(existing);
        
        return Result<Product>.Ok(updateProduct);
    }

    public async Task<Result> DeleteProductByIdAsync(int productId)
    {
        var existing = await _productRepository.GetProductByIdAsync(productId);
        if (existing is null) return Result.Fail("Product not found");
        
        await _productRepository.DeleteProductByIdAsync(existing);
        return Result.Ok();
    }
}