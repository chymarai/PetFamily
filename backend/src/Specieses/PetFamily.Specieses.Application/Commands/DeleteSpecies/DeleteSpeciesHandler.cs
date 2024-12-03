using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PetFamily.Core.Abstraction;
using PetFamily.Core.Extensions;
using PetFamily.SharedKernel;
using PetFamily.Volunteers.Contracts;

namespace PetFamily.Specieses.Application.Commands.DeleteSpecies;
public class DeleteSpeciesHandler : ICommandHandler<Guid, DeleteSpeciesCommand>
{
    private readonly ISpeciesesRepository _speciesesRepository;
    private readonly IVolunteerContract _volunteerContract;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<DeleteSpeciesHandler> _logger;
    private readonly IValidator<DeleteSpeciesCommand> _validator;

    public DeleteSpeciesHandler(
        ISpeciesesRepository speciesesRepository,
        IVolunteerContract volunteerContract,
        IUnitOfWork unitOfWork,
        ILogger<DeleteSpeciesHandler> logger,
        IValidator<DeleteSpeciesCommand> validator)
    {
        _speciesesRepository = speciesesRepository;
        _volunteerContract = volunteerContract;
        _unitOfWork = unitOfWork;
        _logger = logger;
        _validator = validator;
    }
    public async Task<Result<Guid, ErrorList>> Handle(DeleteSpeciesCommand command, CancellationToken token)
    {
        var validationResult = await _validator.ValidateAsync(command, token);
        if (validationResult.IsValid == false)
            return validationResult.ToErrorList();

        var petResult = await _volunteerContract.PetUsedSpecies(command.SpeciesId, token);
        if (petResult.Value != null)
            return Errors.Species.Exist(command.SpeciesId).ToErrorList();

        var speciesResult = await _speciesesRepository.GetById(command.SpeciesId, token);
        if (speciesResult.IsFailure)
            return speciesResult.Error.ToErrorList();

        var result = _speciesesRepository.Delete(speciesResult.Value, token);

        await _unitOfWork.SaveChanges(token);

        _logger.LogInformation("Updates deleted with id {speciesId}", speciesResult);

        return result;
    }
}
