﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PetFamily.Accounts.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PetFamily.Accounts.Infrastructure;
public class AccountsSeeder
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public AccountsSeeder(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    public async Task SeedAsync()
    {
        var json = await File.ReadAllTextAsync("etc/accounts.json");

        using var scope = _serviceScopeFactory.CreateScope();

        var accountsContext = scope.ServiceProvider.GetRequiredService<AccountsDbContext>();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<Role>>();
        var permissionManager = scope.ServiceProvider.GetRequiredService<PermissionManagerпа>();

        var seedData = JsonSerializer.Deserialize<RolePermissionConfig>(json);

        var permissionToAdd = seedData.Permissions.SelectMany(permissionGroup => permissionGroup.Value);

        foreach ( var permissionCode in permissionToAdd )
        {
            await AddPermission(accountsContext, permissionCode);
        }

        await accountsContext.SaveChangesAsync();
    }

    private static async Task AddPermission(AccountsDbContext accountsContext, string? permissionCode)
    {
        var isPermissionExist = await accountsContext.Permissions.AnyAsync(p => p.Code == permissionCode);
        if (isPermissionExist)
            return;

        await accountsContext.Permissions.AddAsync(new Permission { Code = permissionCode });
    }
}

public class RolePermissionConfig
{
    public Dictionary<string, string[]> Permissions { get; set; } = [];
    public Dictionary<string, string[]> Roles { get; set; } = [];
}