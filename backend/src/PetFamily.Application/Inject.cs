﻿using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using PetFamilty.Shared.Abstraction;
using PetFamily.SharedKernel.Abstraction;

namespace PetFamily.Application;

public static class Inject
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services
            .AddCommands()
            .AddQueries()
            .AddValidatorsFromAssembly(typeof(Inject).Assembly);

        return services;
    }

    private static IServiceCollection AddQueries(this IServiceCollection services)
    {
        return services.Scan(scan => scan.FromAssemblies(typeof(Inject).Assembly)
           .AddClasses(classes => classes.
               AssignableToAny(typeof(IQueriesHandler<,>), typeof(IQueriesHandler<>)))
           .AsSelfWithInterfaces()
           .WithScopedLifetime());
    }

    private static IServiceCollection AddCommands(this IServiceCollection services)
    {
        return services.Scan(scan => scan.FromAssemblies(typeof(Inject).Assembly)
            .AddClasses(classes => classes.
                AssignableToAny(typeof(ICommandHandler<,>), typeof(ICommandHandler<>)))
            .AsSelfWithInterfaces()
            .WithScopedLifetime());
    }
}
