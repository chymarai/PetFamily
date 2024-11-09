using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using PetFamily.Application.Pet.AddFiles;
using PetFamily.Application.PetCreate.Create;
using PetFamily.Application.Specieses.Create;
using PetFamily.Application.Specieses.CreateBreed;
using PetFamily.Application.Volunteers.CreateVolunteer;
using PetFamily.Application.Volunteers.Delete;
using PetFamily.Application.Volunteers.UpdateMainInfo;
using PetFamily.Application.Volunteers.UpdateSocialNetwork;
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

        services.AddValidatorsFromAssembly(typeof(Inject).Assembly);

        return services;
    }
}
