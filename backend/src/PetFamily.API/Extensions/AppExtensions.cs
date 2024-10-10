using Microsoft.EntityFrameworkCore;
using PetFamily.Infrastructure;

namespace PetFamily.API.Extensions;

public static class AppExtensions //авто создание миграций при запуске приложения
{
    public static void ApplyMigration(this WebApplication app)
    {
        using var scope = app.Services.CreateAsyncScope();

        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        dbContext.Database.Migrate();
    }
}
