using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PetFamily.Core.Abstraction;
using PetFamily.Core.Extensions;
using PetFamily.SharedKernel;

namespace PetFamily.Volunteers.Application.Commands.SoftDeletePet;
public class SoftDeletePetHandler : ICommandHandler<Guid, SoftDeletePetCommand>
{
    private readonly IWriteVolunteersRepository _volunteersRepository;
    private readonly IValidator<SoftDeletePetCommand> _validator;
    private readonly ILogger<SoftDeletePetHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public SoftDeletePetHandler(
        IWriteVolunteersRepository volunteersRepository,
        IValidator<SoftDeletePetCommand> validator,
        ILogger<SoftDeletePetHandler> logger,
        IUnitOfWork unitOfWork)
    {
        _volunteersRepository = volunteersRepository;
        _validator = validator;
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid, ErrorList>> Handle(SoftDeletePetCommand command, CancellationToken token)
    {
        var validationResult = await _validator.ValidateAsync(command, token);
        if (validationResult.IsValid == false)
            return validationResult.ToErrorList();

        var volunteerResult = await _volunteersRepository.GetById(command.VolunteerId, token);
        if (volunteerResult.IsFailure)
            return volunteerResult.Error.ToErrorList();

        var petResult = volunteerResult.Value.GetPetById(command.PetId).Value;

        petResult.Delete();

        await _unitOfWork.SaveChanges(token);

        _logger.LogInformation("Updates deleted with id {petId}", petResult);

        return petResult.Id.Value;
    }
}
