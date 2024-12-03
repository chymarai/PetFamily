using System.Data;

namespace PetFamily.Core.Abstraction;
public interface IUnitOfWork
{
    Task<IDbTransaction> BeginTransaction(CancellationToken token = default);

    Task SaveChanges(CancellationToken token = default);
}
