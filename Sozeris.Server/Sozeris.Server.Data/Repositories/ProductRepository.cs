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

    public async Task<IReadOnlyList<Product>> GetAllProductsAsync(CancellationToken ct)
    {
        return await _context.Products.AsNoTracking().ToListAsync(ct);
    }

    public async Task<Product?> GetProductByIdAsync(int productId, CancellationToken ct)
    {
        return await _context.Products.AsNoTracking().FirstOrDefaultAsync(x => x.Id == productId, ct);
    }

    public async Task<Product> AddProductAsync(Product product, CancellationToken ct)
    {
        await _context.Products.AddAsync(product, ct);
        await _context.SaveChangesAsync(ct);
        return product;
    }

    public async Task<Product> UpdateProductAsync(Product product, CancellationToken ct)
    {
        _context.Products.Update(product);
        await _context.SaveChangesAsync(ct);
        return product;
    }

    public async Task DeleteProductByIdAsync(Product product, CancellationToken ct)
    {
        _context.Products.Remove(product);
        await _context.SaveChangesAsync(ct);
    }
}