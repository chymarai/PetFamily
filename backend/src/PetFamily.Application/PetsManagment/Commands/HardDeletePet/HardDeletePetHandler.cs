using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using PetFamily.Application.Abstraction;
using PetFamily.Application.Database;
using PetFamily.Application.Extensions;
using PetFamily.Application.FileProvider;
using PetFamily.Application.PetsManagment.Queries;
using PetFamily.Domain.PetsManagment.ValueObjects.Pets;
using PetFamily.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PetFamily.Application.PetsManagment.Commands.HardDeletePet;
public class HardDeletePetHandler : ICommandHandler<Guid, HardDeletePetCommand>
{
    private const string BUCKET_NAME = "photos";

    private readonly IWriteVolunteersRepository _volunteersRepository;
    private readonly IValidator<HardDeletePetCommand> _validator;
    private readonly IFileProvider _fileProvider;
    private readonly ILogger<HardDeletePetHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public HardDeletePetHandler(
        IWriteVolunteersRepository volunteersRepository,
        IValidator<HardDeletePetCommand> validator,
        IFileProvider fileProvider,
        ILogger<HardDeletePetHandler> logger,
        IUnitOfWork unitOfWork)
    {
        _volunteersRepository = volunteersRepository;
        _validator = validator;
        _fileProvider = fileProvider;
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid, ErrorList>> Handle(HardDeletePetCommand command, CancellationToken token)
    {
        var validationResult = await _validator.ValidateAsync(command, token);
        if (validationResult.IsValid == false)
            return validationResult.ToErrorList();

        var volunteerResult = await _volunteersRepository.GetById(command.VolunteerId, token);
        if (volunteerResult.IsFailure)
            return volunteerResult.Error.ToErrorList();

        var petResult = volunteerResult.Value.GetPetById(command.PetId).Value;

        volunteerResult.Value.HardDeletePet(petResult);

        var files = petResult.Files;

        foreach (var file in files)
        {
            var filesPath = file.PathToStorage;

            var fileInfos = new FileInfos(filesPath, BUCKET_NAME);

            await _fileProvider.RemoveFiles(fileInfos, token);
        }

        await _unitOfWork.SaveChanges(token);

        _logger.LogInformation("Updates hard deleted with id {petId}", petResult);
        
        return petResult.Id.Value;
    }
}

