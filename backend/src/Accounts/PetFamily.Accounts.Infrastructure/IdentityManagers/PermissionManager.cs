using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PetFamily.Accounts.Domain;

namespace PetFamily.Accounts.Infrastructure.IdentityManagers;

public class PermissionManager(AccountsDbContext accountsDbContext)
{
    public async Task<Permission?> FindByCode(string code)
        => await accountsDbContext.Permissions.FirstOrDefaultAsync(p => p.Code == code);

    public async Task AddRangeIfExist(IEnumerable<string> permissions)
    {
        foreach (var permissionCode in permissions)
        {

            var isPermissionExist = await accountsDbContext.Permissions
                .AnyAsync(p => p.Code == permissionCode);

            if (isPermissionExist)
                return;

            await accountsDbContext.Permissions.AddAsync(new Permission { Code = permissionCode });
        }

        await accountsDbContext.SaveChangesAsync();
    }

    public async Task<HashSet<string>> GetUserPermissionCodes(Guid userId, CancellationToken cancellationToken)
    {
        var permissions = await accountsDbContext.Users
            .Include(u => u.Roles)
            .Where(u => u.Id == userId)
            .SelectMany(u => u.Roles)
            .SelectMany(r => r.RolePermissions)
            .Select(rp => rp.Permission.Code)
            .ToListAsync();

        return permissions.ToHashSet();
    }
}
