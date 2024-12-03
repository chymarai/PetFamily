using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PetFamily.Core.Abstraction;
using PetFamily.Core.Extensions;
using PetFamily.SharedKernel;
using PetFamily.SharedKernel.Ids;
using PetFamily.Specieses.Domain;

namespace PetFamily.Specieses.Application.Commands.CreateSpecies;
public class CreateSpeciesHandler : ICommandHandler<Guid, CreateSpeciesCommand>
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
