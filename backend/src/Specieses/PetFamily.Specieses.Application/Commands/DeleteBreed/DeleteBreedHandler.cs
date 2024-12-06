using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PetFamily.Core.Abstraction;
using PetFamily.Core.Extensions;
using PetFamily.SharedKernel;
using PetFamily.SharedKernel.Ids;
using PetFamily.Volunteers.Contracts;

namespace PetFamily.Specieses.Application.Commands.DeleteBreed;
public class DeleteBreedHandler : ICommandHandler<Guid, DeleteBreedCommand>
{
    private readonly ISpeciesesRepository _speciesesRepository;
    private readonly IVolunteerContract _volunteerContract;
    private readonly IValidator<DeleteBreedCommand> _validator;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<DeleteBreedCommand> _logger;

    public DeleteBreedHandler(
        ISpeciesesRepository speciesesRepository,
        IVolunteerContract volunteerContract,
        IValidator<DeleteBreedCommand> validator,
        IUnitOfWork unitOfWork,
        ILogger<DeleteBreedCommand> logger
        )
    {
        _speciesesRepository = speciesesRepository;
        _volunteerContract = volunteerContract;
        _validator = validator;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Result<Guid, ErrorList>> Handle(DeleteBreedCommand command, CancellationToken token)
    {
        var validationResult = await _validator.ValidateAsync(command, token);
        if (validationResult.IsValid == false)
            return validationResult.ToErrorList();

        var breedId = BreedId.Create(command.BreedId).Value;

        var petResult = await _volunteerContract.PetUsedBreed(breedId, token);
        if (petResult.Value != null)
            return Errors.Breed.Exist(breedId).ToErrorList();

        var speciesResult = await _speciesesRepository.GetById(command.SpeciesId);
        if (speciesResult.IsFailure)
            return speciesResult.Error.ToErrorList();

        var breed = speciesResult.Value.Breeds.FirstOrDefault(b => b.Id.Value == breedId);
        if (breed is null)
            return Errors.General.NotFound().ToErrorList();

        speciesResult.Value.DeleteBreed(breed);

        await _unitOfWork.SaveChanges(token);

        _logger.LogInformation("Updates deleted with id {breedId}", breedId);

        return breedId;
    }
}
