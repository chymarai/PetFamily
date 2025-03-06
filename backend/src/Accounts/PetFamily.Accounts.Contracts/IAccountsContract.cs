using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Accounts.Contracts;
public  interface IAccountsContract
{
    Task<HashSet<string>> GetUserPermissionCodes(Guid userId, CancellationToken token);
}
