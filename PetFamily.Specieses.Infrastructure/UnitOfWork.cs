using Microsoft.EntityFrameworkCore.Storage;
using PetFamily.Core.Abstraction;
using PetFamily.Specieses.Infrastructure.DbContexts;
using System.Data;

namespace PetFamily.Specieses.Infrastructure;
public class UnitOfWork : IUnitOfWork //Управление транзакциями
{
    private readonly WriteDbContext _dbContext;
    public UnitOfWork(WriteDbContext dbContext)
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
