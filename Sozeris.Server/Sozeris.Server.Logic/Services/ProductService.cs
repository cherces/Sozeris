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

    public async Task<IReadOnlyList<Product>> GetAllProductsAsync(CancellationToken ct)
    {
        var products = await _productRepository.GetAllProductsAsync(ct);
        
        return products;
    }

    public async Task<Result<Product>> GetProductByIdAsync(int productId, CancellationToken ct)
    {
        var result = await _productRepository.GetProductByIdAsync(productId, ct);
        if (result == null) return Result<Product>.Fail(DomainError.NotFound("Product", productId));
        
        return Result<Product>.Ok(result);
    }

    public async Task<Result<Product>> AddProductAsync(Product product, CancellationToken ct)
    {
        var newProduct = await _productRepository.AddProductAsync(product, ct);
        
        return Result<Product>.Ok(newProduct);
    }

    public async Task<Result<Product>> UpdateProductAsync(int productId, Product product, CancellationToken ct)
    {
        var existing = await _productRepository.GetProductByIdAsync(productId, ct);
        if (existing is null) return Result<Product>.Fail(DomainError.NotFound("Product", productId));

        existing.CloneFrom(product);
        var updateProduct = await _productRepository.UpdateProductAsync(existing, ct);
        
        return Result<Product>.Ok(updateProduct);
    }

    public async Task<Result> DeleteProductByIdAsync(int productId, CancellationToken ct)
    {
        var existing = await _productRepository.GetProductByIdAsync(productId, ct);
        if (existing is null) return Result.Fail(DomainError.NotFound("Product", productId));
        
        await _productRepository.DeleteProductByIdAsync(existing, ct);
        return Result.Ok();
    }
}