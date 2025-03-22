using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using PetFamily.Accounts.Application;
using PetFamily.Accounts.Domain;
using PetFamily.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Accounts.Infrastructure.IdentityManagers;

public class RefreshSessionManager(AccountsDbContext accountsDbContext) : IRefreshSessionManager
{
    public async Task<Result<RefreshSession, Error>> GetByRefreshToken(
        Guid refreshToken, CancellationToken cancellationToken)
    {
        var refreshSession = await accountsDbContext.RefreshSessions.FirstOrDefaultAsync(r => r.RefreshToken == refreshToken, cancellationToken);

        if (refreshSession == null)
            return Errors.General.NotFound(refreshToken);

        return refreshSession;
    }
}
