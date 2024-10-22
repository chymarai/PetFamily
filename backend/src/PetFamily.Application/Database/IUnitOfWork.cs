﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Application.Database;
public interface IUnitOfWork
{
    Task<IDbTransaction> BeginTransaction(CancellationToken token = default);

    Task SaveChanges(CancellationToken token = default);
}
