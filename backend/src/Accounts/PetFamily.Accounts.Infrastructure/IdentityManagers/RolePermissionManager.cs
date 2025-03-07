using Microsoft.EntityFrameworkCore;
using PetFamily.Accounts.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Accounts.Infrastructure.IdentityManagers;
public class RolePermissionManager(AccountsDbContext accountsDbContext)
{
    public async Task AddRangeIfExist(Guid roleId, IEnumerable<string> permissions)
    {
        foreach (var permissionCode in permissions)
        {
            var permission = await accountsDbContext.Permissions.FirstOrDefaultAsync(p => p.Code == permissionCode)
                             ?? throw new ApplicationException($"Permission code {permissionCode} is not found");

            var rolePermissionExist = await accountsDbContext.RolePermissions.
                AnyAsync(rp => rp.RoleId == roleId && rp.PermissionId == permission!.Id);

            if (rolePermissionExist)
                continue;

            accountsDbContext.RolePermissions.Add(new RolePermission
            {
                RoleId = roleId,
                PermissionId = permission!.Id
            });
        }

        await accountsDbContext.SaveChangesAsync();
    }
}
