using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PetFamily.Core.BackgroundServices;
using PetFamily.Specieses.Application;
using PetFamily.Specieses.Contracts;
using PetFamily.Specieses.Infrastructure.DbContexts;
namespace PetFamily.Specieses.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddSpeciesInfrastructure(
        this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddDbContexts()
            .AddRepositories()
            .AddDatabase()
            .AddContracts();

        return services;
    }

    private static IServiceCollection AddContracts(this IServiceCollection services)
    {
        services.AddScoped<ISpeciesContract, SpeciesContract>();

        return services;
    }

    private static IServiceCollection AddDbContexts(this IServiceCollection services)
    {
        services.AddScoped<WriteDbContext>();
        services.AddScoped<IReadDbContext, ReadDbContext>();

        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<ISpeciesesRepository, SpeciesesRepository>();

        return services;
    }

    private static IServiceCollection AddDatabase(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, SpeciesUnitOfWork>();

        return services;
    }
}
