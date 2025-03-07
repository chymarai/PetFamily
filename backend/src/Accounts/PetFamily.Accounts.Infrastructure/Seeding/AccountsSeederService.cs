using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PetFamily.Accounts.Domain;
using PetFamily.Accounts.Infrastructure.IdentityManagers;
using PetFamily.Accounts.Infrastructure.Options;
using PetFamily.SharedKernel.ValueObjects;
using PetFamily.Volunteers.Domain.VolunteersValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PetFamily.Accounts.Infrastructure.Seeding;
public class AccountsSeederService(
    UserManager<User> userManager,
    RoleManager<Role> roleManager,
    PermissionManager permissionManager,
    RolePermissionManager rolePermissionManager,
    AdminAccountManager adminAccountManager,
    IOptions<AdminOptions> adminOptions,
    ILogger<AccountsSeederService> logger)
{
    private readonly AdminOptions _adminOptions = adminOptions.Value;

    public async Task SeedAsync()
    {
        var json = await File.ReadAllTextAsync("etc/accounts.json");

        var seedData = JsonSerializer.Deserialize<RolePermissionOptions>(json)
                       ?? throw new ArgumentNullException("Could not deserilize role permission config");

        await SeedPermissions(seedData);

        await SeedRoles(seedData);

        await SeedRolePermissions(seedData);

        await SeedAdmin();
    }

    private async Task SeedAdmin()
    {
        var adminRole = await roleManager.FindByNameAsync(AdminAccount.ADMIN)
                        ?? throw new ApplicationException("Could not find admin role.");

        var adminUser = User.CreateAdmin(_adminOptions.UserName, _adminOptions.Email, adminRole);
        await userManager.CreateAsync(adminUser, _adminOptions.Password);
        
        var fullName = FullName.Create(_adminOptions.UserName, _adminOptions.UserName, _adminOptions.UserName).Value;
        var email = Email.Create(_adminOptions.Email).Value;

        var adminAccount = AdminAccount.Create(adminUser, fullName, email);

        await adminAccountManager.CreateAdminAccount(adminAccount);

        logger.LogInformation("Admin account added to database.");
    }

    private async Task SeedRoles(RolePermissionOptions seedData)
    {
        foreach (var roleName in seedData.Roles.Keys)
        {
            var role = await roleManager.FindByNameAsync(roleName);

            if (role == null)
            {
                await roleManager.CreateAsync(new Role { Name = roleName });
            }
        }

        logger.LogInformation("Roles added to database.");
    }

    private async Task SeedPermissions(RolePermissionOptions seedData)
    {
        var permissionsToAdd = seedData.Permissions.SelectMany(permissionGroup => permissionGroup.Value);

        await permissionManager.AddRangeIfExist(permissionsToAdd);

        logger.LogInformation("Permissions added to database.");
    }

    private async Task SeedRolePermissions(RolePermissionOptions seedData)
    {
        foreach (var roleName in seedData.Roles.Keys)
        {
            var role = await roleManager.FindByNameAsync(roleName);

            var rolePermissions = seedData.Roles[roleName];

            await rolePermissionManager.AddRangeIfExist(role!.Id, rolePermissions);
        }

        logger.LogInformation("Role permissions added to database.");
    }
}
