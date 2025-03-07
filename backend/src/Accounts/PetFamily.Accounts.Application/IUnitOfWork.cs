using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace PetFamily.Accounts.Application;
public interface IUnitOfWork
{
    Task<IDbTransaction> BeginTransaction(CancellationToken token = default);

    Task SaveChanges(CancellationToken token = default);
}
