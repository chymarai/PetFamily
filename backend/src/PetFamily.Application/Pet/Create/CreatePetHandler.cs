using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using PetFamily.Application.Database;
using PetFamily.Application.DTOs;
using PetFamily.Application.FileProvider;
using PetFamily.Application.Pet.Create;
using PetFamily.Domain.PetsManagment.Ids;
using PetFamily.Domain.PetsManagment.Entities;
using PetFamily.Domain.PetsManagment.ValueObjects.Shared;
using PetFamily.Domain.Shared;
using PetFamily.Domain.SpeciesManagment;
using PetFamily.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetFamily.Domain.PetsManagment.ValueObjects.Pets;
using System.Transactions;
using System.Data.Common;
using PetFamily.Application.Specieses;

namespace PetFamily.Application.Pet.Create;
public class CreatePetHandler
{
    private const string BUCKET_NAME = "photos";

    private readonly IVolunteersRepository _volunteersRepository;
    private readonly IFileProvider _fileProvider;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<CreatePetHandler> _logger;

    public CreatePetHandler(
        IVolunteersRepository volunteersRepository,
        IFileProvider fileProvider, 
        IUnitOfWork unitOfWork,
        ILogger<CreatePetHandler> logger)
    {
        _volunteersRepository = volunteersRepository;
        _fileProvider = fileProvider;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Result<Guid, Error>> Handle(CreatePetCommand command, CancellationToken token)
    {
        var transaction = await _unitOfWork.BeginTransaction(token);

        try
        {
            var volunteerResult = await _volunteersRepository.GetById(command.VolunteerId);
            
            if (volunteerResult.IsFailure)
                return volunteerResult.Error;

            var name = Name.Create(command.Name).Value;
            var description = Description.Create(command.Description).Value;

            var speciesId = SpeciesId.NewSpeciesId().Value;
            var breedId = BreedId.NewBreedId().Value;
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

            var birthdate = Birthdate.Create(command.BirthDate).Value;

            var requisite = RequisiteDetails.Create(command.RequisiteDetails.Requisite
               .Select(r => Requisite.Create(r.Name, r.Description).Value));

            var petId = PetId.NewPetId();

            List<FileData> filesData = [];

            foreach (var file in command.Files)
            {
                var extension = Path.GetExtension(file.FileName); //Получаем расширение файла из пути файла

                var filePath = FilePath.Create(Guid.NewGuid(), extension);

                if (filePath.IsFailure)
                    return filePath.Error;

                var fileContent = new FileData(file.Stream, filePath.Value, BUCKET_NAME);

                filesData.Add(fileContent);
            }

            var petFiles = filesData.
                Select(f => f.FilePath)
                .Select(f => new PetFiles(f))
                .ToList();

            var pet = new Domain.PetsManagment.Entities.Pet(
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
                birthdate,
                command.DateOfCreation,
                requisite,
                petFiles);

            volunteerResult.Value.AddPet(pet);

            await _unitOfWork.SaveChanges(token); 

            var uploadResult = await _fileProvider.UploadFiles(filesData, token);

            if (uploadResult.IsFailure)
                return uploadResult.Error;

            transaction.Commit(); //файлы в бд сохраняются в этот момент

            return pet.Id.Value;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,
                "Can not add issue to module - {id} in transaction", command.VolunteerId);

            transaction.Rollback();

            return Error.Failure("Can not add issue to module - {id}", "module.issue.failure");
        }

    }
}
