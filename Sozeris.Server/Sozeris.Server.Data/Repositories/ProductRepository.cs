using Microsoft.EntityFrameworkCore;
using Sozeris.Server.Data.DbContext;
using Sozeris.Server.Data.Repositories.Interfaces;
using Sozeris.Server.Models.Entities;

namespace Sozeris.Server.Data.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly ApplicationDbContext _context;

    public ProductRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Product>> GetAllProductsAsync()
    {
        var products = await _context.Products.ToListAsync();
        
        return products;
    }

    public async Task<Product?> GetProductByIdAsync(int productId)
    {
        var product = await _context.Products.FindAsync(productId);
        
        return product;
    }

    public async Task<bool> AddProductAsync(Product product)
    {
        var newProduct = await _context.Products.AddAsync(product);
        
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> UpdateProductAsync(Product product)
    {
        var oldProduct = await _context.Products.FindAsync(product.Id);
        oldProduct ??= product;
        _context.Products.Update(oldProduct);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteProductByIdAsync(int productId)
    {
        var oldProduct = await _context.Products.FindAsync(productId);
        _context.Products.Remove(oldProduct);
        return await _context.SaveChangesAsync() > 0;
    }
}