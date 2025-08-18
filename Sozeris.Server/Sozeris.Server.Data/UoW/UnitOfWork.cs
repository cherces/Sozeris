using Sozeris.Server.Data.DbContext;
using Sozeris.Server.Models.Interfaces.UoW;

namespace Sozeris.Server.Data.UoW;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
    }

    public int Commit()
    {
        return _context.SaveChanges();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}