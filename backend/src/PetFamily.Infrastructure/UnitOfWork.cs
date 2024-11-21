﻿using Microsoft.EntityFrameworkCore.Storage;
using PetFamily.Application.Database;
using PetFamily.Infrastructure.DbContexts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Infrastructure;
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
