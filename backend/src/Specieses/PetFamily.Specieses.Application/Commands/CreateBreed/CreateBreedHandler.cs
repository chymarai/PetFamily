using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PetFamily.Core.Abstraction;
using PetFamily.Core.Extensions;
using PetFamily.SharedKernel;
using PetFamily.SharedKernel.Ids;
using PetFamily.Specieses.Domain;

namespace PetFamily.Specieses.Application.Commands.CreateBreed;
public class CreateBreedHandler : ICommandHandler<Guid, CreateBreedCommand>
{
    private readonly ISpeciesesRepository _speciesesRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<CreateBreedCommand> _validator;
    private readonly ILogger<CreateBreedCommand> _logger;

    public CreateBreedHandler(
        ISpeciesesRepository speciesesRepository,
        IUnitOfWork unitOfWork,
        IValidator<CreateBreedCommand> validator,
        ILogger<CreateBreedCommand> logger)
    {
        _speciesesRepository = speciesesRepository;
        _unitOfWork = unitOfWork;
        _validator = validator;
        _logger = logger;
    }

    public async Task<Result<Guid, ErrorList>> Handle(
        CreateBreedCommand command,
        CancellationToken token)
    {
        var validationResult = await _validator.ValidateAsync(command, token);

        if (validationResult.IsValid == false)
            return validationResult.ToErrorList();

        var speciesResult = await _speciesesRepository.GetById(command.SpeciesId, token);

        if (speciesResult.IsFailure)
            return speciesResult.Error.ToErrorList();

        var breedId = BreedId.NewBreedId();

        var breedName = BreedName.Create(command.BreedName).Value;

        var breed = new Breed(breedId, breedName);

        speciesResult.Value.AddBreed(breed);

        await _unitOfWork.SaveChanges(token);

        return breedId.Value;
    }
}
