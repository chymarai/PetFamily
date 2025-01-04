using PetFamily.Accounts.Domain;

namespace PetFamily.Accounts.Infrastructure.IdentityManagers;

public class AdminAccountManager(AccountsDbContext accountsDbContext)
{
    public async Task CreateAdminAccount(AdminAccount adminAccount)
    {
        await accountsDbContext.AdminAccounts.AddAsync(adminAccount);

        await accountsDbContext.SaveChangesAsync();
    }
}