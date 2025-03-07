using PetFamily.Accounts.Contracts;
using PetFamily.Accounts.Infrastructure.IdentityManagers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Accounts.Presentation;
public class AccountsContract : IAccountsContract
{
    private readonly PermissionManager _permissionManager;

    public AccountsContract(PermissionManager permissionManager)
    {
        _permissionManager = permissionManager;
    }
    public async Task<HashSet<string>> GetUserPermissionCodes(Guid userId, CancellationToken token)
    {
        return await _permissionManager.GetUserPermissionCodes(userId, token);
    }
}
