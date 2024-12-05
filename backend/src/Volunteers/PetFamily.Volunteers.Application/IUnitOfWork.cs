using System.Data;

namespace PetFamily.Volunteers.Application;
public interface IUnitOfWork
{
    Task<IDbTransaction> BeginTransaction(CancellationToken token = default);

    Task SaveChanges(CancellationToken token = default);
}
