using Microsoft.EntityFrameworkCore;
using Sozeris.Server.Data.DbContext;
using Sozeris.Server.Domain.Entities;
using Sozeris.Server.Domain.Interfaces.Repositories;

namespace Sozeris.Server.Data.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly ApplicationDbContext _context;

    public ProductRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<Product>> GetAllProductsAsync()
    {
        return await _context.Products.AsNoTracking().ToListAsync();
    }

    public async Task<Product?> GetProductByIdAsync(int productId)
    {
        return await _context.Products.FindAsync(productId);
    }

    public async Task<Product?> AddProductAsync(Product product)
    {
        _context.Products.Add(product);
        
        var saved = await _context.SaveChangesAsync() > 0;
        
        return saved ? product : null;
    }

    public async Task<bool> UpdateProductAsync(Product product)
    {
        _context.Products.Update(product);
        
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteProductByIdAsync(int productId)
    {
        var oldProduct = await _context.Products.FindAsync(productId);
        
        if (oldProduct is null)
            return false;
        
        _context.Products.Remove(oldProduct);
        
        return await _context.SaveChangesAsync() > 0;
    }
}