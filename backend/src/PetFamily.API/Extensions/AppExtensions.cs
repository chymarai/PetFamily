﻿using Microsoft.EntityFrameworkCore;
using PetFamily.Infrastructure.DbContexts;

namespace PetFamily.API.Extensions;

public static class AppExtensions //авто создание миграций при запуске приложения
{
    public static async void ApplyMigration(this WebApplication app)
    {
        using var scope = app.Services.CreateAsyncScope();

        var dbContext = scope.ServiceProvider.GetRequiredService<WriteDbContext>();

        await dbContext.Database.MigrateAsync();
    }
}
