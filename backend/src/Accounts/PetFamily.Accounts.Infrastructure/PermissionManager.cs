using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PetFamily.Accounts.Domain;

namespace PetFamily.Accounts.Infrastructure;

public class PermissionManager(AccountsDbContext accountsDbContext)
{
    public async Task AddPermission(string permissionCode)
    {
        var isPermissionExist = await accountsDbContext.Permissions.AnyAsync(p => p.Code == permissionCode);
        if (isPermissionExist)
            return;

        await accountsDbContext.Permissions.AddAsync(new Permission { Code = permissionCode });
    }
}