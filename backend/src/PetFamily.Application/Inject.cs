using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using PetFamily.Application.Abstraction;
using PetFamily.Application.Specieses.Create;
using PetFamily.Application.Specieses.CreateBreed;
using PetFamily.Application.Volunteers.Commands.AddFiles;
using PetFamily.Application.Volunteers.Commands.AddPet;
using PetFamily.Application.Volunteers.Queries.GetVolunteersWithPagination;
using PetFamily.Application.Volunteers.WriteHandler.Create;
using PetFamily.Application.Volunteers.WriteHandler.DeleteVolunteer;
using PetFamily.Application.Volunteers.WriteHandler.UpdateMainInfos;
using PetFamily.Application.Volunteers.WriteHandler.UpdateSocialNetwork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
               AssignableToAny(typeof(IQueriesHandler<,>)))
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
