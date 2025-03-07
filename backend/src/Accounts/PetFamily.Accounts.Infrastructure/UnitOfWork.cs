using Microsoft.EntityFrameworkCore.Storage;
using PetFamily.Accounts.Application;
using System.Data;

namespace PetFamily.Accounts.Infrastructure;
public class UnitOfWork : IUnitOfWork //Управление транзакциями
{
    private readonly AccountsDbContext _dbContext;
    public UnitOfWork(AccountsDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<IDbTransaction> BeginTransaction(CancellationToken token = default)
    {
        var transaction = await _dbContext.Database.BeginTransactionAsync(token);
        return transaction.GetDbTransaction();
    }
    public async Task SaveChanges(CancellationToken token = default)
    {
        await _dbContext.SaveChangesAsync(token);
    }
}
