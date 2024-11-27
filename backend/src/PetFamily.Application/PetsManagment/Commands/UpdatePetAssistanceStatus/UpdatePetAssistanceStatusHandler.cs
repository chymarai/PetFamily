using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PetFamily.Application.Abstraction;
using PetFamily.Application.Database;
using PetFamily.Application.Extensions;
using PetFamily.Application.PetsManagment.Queries;
using PetFamily.Domain.PetsManagment.ValueObjects.Pets;
using PetFamily.Domain.PetsManagment.ValueObjects.Volunteers;
using PetFamily.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PetFamily.Application.PetsManagment.Commands.UpdatePetAssistanceStatus;
public class UpdatePetAssistanceStatusHandler : ICommandHandler<Guid, UpdatePetAssistanceStatusCommand>
{
    private readonly IWriteVolunteersRepository _volunteersRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<UpdatePetAssistanceStatusHandler> _logger;
    private readonly IValidator<UpdatePetAssistanceStatusCommand> _validator;

    public UpdatePetAssistanceStatusHandler(
        IWriteVolunteersRepository volunteersRepository,
        IUnitOfWork unitOfWork,
        ILogger<UpdatePetAssistanceStatusHandler> logger,
        IValidator<UpdatePetAssistanceStatusCommand> validator)
    {
        _volunteersRepository = volunteersRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
        _validator = validator;
    }
    public async Task<Result<Guid, ErrorList>> Handle(UpdatePetAssistanceStatusCommand command, CancellationToken token)
    {
        var validationResult = await _validator.ValidateAsync(command, token);
        if (validationResult.IsValid == false)
            return validationResult.ToErrorList();

        var volunteerResult = await _volunteersRepository.GetById(command.VolunteerId, token);
        if (volunteerResult.IsFailure)
            return volunteerResult.Error.ToErrorList();

        var petResult = volunteerResult.Value.GetPetById(command.PetId).Value;

        var assistanceStatus = Enum.Parse<AssistanceStatus>(command.AssistanceStatus, true);

        petResult.UpdateAssistanceStatus(assistanceStatus);
        await _unitOfWork.SaveChanges(token);

        _logger.LogInformation("Update {assistanceStatus} with id {petId}", assistanceStatus, petResult.Id);

        return petResult.Id.Value;
    }
}
