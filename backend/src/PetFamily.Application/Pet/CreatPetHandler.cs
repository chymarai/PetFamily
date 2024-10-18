using CSharpFunctionalExtensions;
using PetFamily.Application.DTOs;
using PetFamily.Application.FileProvider;
using PetFamily.Domain.PetsManagment.Entities;
using PetFamily.Domain.PetsManagment.Ids;
using PetFamily.Domain.PetsManagment.ValueObjects.Pets;
using PetFamily.Domain.PetsManagment.ValueObjects.Shared;
using PetFamily.Domain.Shared;
using PetFamily.Domain.SpeciesManagment;
using PetFamily.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Application.Pets;
public class CreatPetHandler
{
    private readonly IVolunteersRepository _volunteersRepository;
    private readonly IFileProvider _fileProvider;

    public CreatPetHandler(IVolunteersRepository volunteersRepository,IFileProvider fileProvider)
    {
        _volunteersRepository = volunteersRepository;
        _fileProvider = fileProvider;
    }

    public async Task<Result<Guid, Error>> Handle(CreatPetCommand command, CancellationToken token)
    {
        var volunteerResult = await _volunteersRepository.GetById(command.VolunteerId);
        if (volunteerResult.IsFailure)
            return volunteerResult.Error;

        var name = Name.Create(command.Name).Value;
        var description = Description.Create(command.Description).Value;

        var speciesId = SpeciesId.Create(command.SpeciesBreed.SpeciesId).Value;
        var breedId = BreedId.Create(command.SpeciesBreed.BreedId).Value;
        var speciesBreed = SpeciesBreed.Create(speciesId, breedId).Value;

        var color = Color.Create(command.Color).Value;
        var healthInformation = HealthInformation.Create(command.HealthInfornmation).Value;

        var address = Address.Create(
            command.Address.country,
            command.Address.region,
            command.Address.city,
            command.Address.street).Value;

        var weight = Weight.Create(command.Weight).Value;
        var height = Height.Create(command.Height).Value;
        var phoneNumber = PhoneNumber.Create(command.PhoneNumber).Value;

        var assistanceStatus = AssistanceStatus.AtHome;

        var requisite = RequisiteDetails.Create(command.RequisiteDetails.Requisite
           .Select(r => Requisite.Create(r.Name, r.Description).Value));

        var photo = Gallery.Create(command.Gallery.Photo
            .Select(r => PetPhoto.Create(r.Storage, r.IsMain).Value));

        var petId = PetId.NewPetId();

        var pet = new Pet(
            petId,
            name,
            description,
            speciesBreed,
            color,
            healthInformation,
            address,
            weight,
            height,
            phoneNumber,
            command.IsCastrated,
            command.IsVaccination,
            assistanceStatus,
            command.BirthDate,
            command.DateOfCreation,
            requisite,
            photo);

        return petId.Value;
    }
}

//PetId petId,
//        Name name,
//        Description description,
//        SpeciesBreed speciesBreed,
//        Color color,
//        HealthInformation healthInformation,
//        Address address,
//        Weight weight,
//        Height height,
//        PhoneNumber phoneNumber,
//        bool isCastrated,
//        bool isVaccination,
//        AssistanceStatus assistanceStatus,
//        DateOnly birthDate,
//        DateTime dateOfCreation,
//        RequisiteDetails requisiteDetails,
//        Gallery gallery