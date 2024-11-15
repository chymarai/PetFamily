using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using PetFamily.Application.Pet.AddFiles;
using PetFamily.Application.PetCreate.Create;
using PetFamily.Application.Specieses.Create;
using PetFamily.Application.Specieses.CreateBreed;
using PetFamily.Application.Volunteers.Queries.GetVolunteersWithPagination;
using PetFamily.Application.Volunteers.WriteHandler.Create;
using PetFamily.Application.Volunteers.WriteHandler.DeleteVolunteer;
using PetFamily.Application.Volunteers.WriteHandler.UpdateMainInfos;
using PetFamily.Application.Volunteers.WriteHandler.UpdateSocialNetwork;
using PetFamily.Infrastructure.Repositories;
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
        services.AddScoped<CreateVolunteerHandler>();

        services.AddScoped<UpdateMainInfoHandler>();

        services.AddScoped<UpdateSocialNetworkHandler>();

        services.AddScoped<DeleteVolunteerHandler>();

        services.AddScoped<CreatePetHandler>();

        services.AddScoped<UploadFilesToPetHandler>();

        services.AddScoped<CreateSpeciesHandler>();

        services.AddScoped<CreateBreedHandler>();

        services.AddScoped<GetVolunteersWithPaginationHandler>();

        services.AddValidatorsFromAssembly(typeof(Inject).Assembly);

        return services;
    }
}
