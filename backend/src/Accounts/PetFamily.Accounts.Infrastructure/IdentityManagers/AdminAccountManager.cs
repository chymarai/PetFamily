using Microsoft.EntityFrameworkCore;
using PetFamily.Accounts.Domain;

namespace PetFamily.Accounts.Infrastructure.IdentityManagers;

public class AdminAccountManager(AccountsDbContext accountsDbContext)
{
    public async Task CreateAdminAccount(AdminAccount adminAccount)
    {
        await accountsDbContext.AdminAccounts.AddAsync(adminAccount);

        await accountsDbContext.SaveChangesAsync();
    }

    public async Task<bool> SearchAdminAccount()
    {
        var result =  await accountsDbContext.AdminAccounts.FirstOrDefaultAsync(a => a.FullName.FirstName == AdminAccount.ADMIN);
            if (result == null)
                return false;
            
        return true;
             
    }
}