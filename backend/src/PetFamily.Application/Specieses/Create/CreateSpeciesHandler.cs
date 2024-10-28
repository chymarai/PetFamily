using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PetFamily.Application.Database;
using PetFamily.Application.Extensions;
using PetFamily.Application.Specieses.Create;
using PetFamily.Domain.Modules.Volunteers;
using PetFamily.Domain.PetsManagment.ValueObjects.Volunteers;
using PetFamily.Domain.Shared;
using PetFamily.Domain.SpeciesManagment;
using PetFamily.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Application.Specieses.Create;
public class CreateSpeciesHandler
{
    private readonly ISpeciesesRepository _speciesesRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<CreateSpeciesCommand> _validator;
    private readonly ILogger<CreateSpeciesHandler> _logger;

    public CreateSpeciesHandler(
        ISpeciesesRepository speciesesRepository,
        IUnitOfWork unitOfWork,
        IValidator<CreateSpeciesCommand> validator,
        ILogger<CreateSpeciesHandler> logger)
    {
        _speciesesRepository = speciesesRepository;
        _unitOfWork = unitOfWork;
        _validator = validator;
        _logger = logger;
    }

    public async Task<Result<Guid, ErrorList>> Handle(
        CreateSpeciesCommand command,
        CancellationToken token)
    {
        var validationResult = await _validator.ValidateAsync(command, token);

        if (validationResult.IsValid == false)
            return validationResult.ToErrorList();

        var speciesId = SpeciesId.NewSpeciesId();

        var speciesName = SpeciesName.Create(command.SpeciesName).Value;

        var speciesToCreate = new Species(speciesId, speciesName);

        await _speciesesRepository.Add(speciesToCreate, token);

        await _unitOfWork.SaveChanges(token);

        _logger.LogInformation("Create species {speciesName} with id {speciesId}", speciesName, speciesId);

        return speciesId.Value;
    }
}
