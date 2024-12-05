using System.Data;

namespace PetFamily.Specieses.Application;
public interface IUnitOfWork
{
    Task<IDbTransaction> BeginTransaction(CancellationToken token = default);

    Task SaveChanges(CancellationToken token = default);
}
