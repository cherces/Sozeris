using Sozeris.Server.Domain.Interfaces.Repositories;

namespace Sozeris.Server.Models.Interfaces.UoW;

public interface IUnitOfWork : IDisposable
{
    int Commit();
}