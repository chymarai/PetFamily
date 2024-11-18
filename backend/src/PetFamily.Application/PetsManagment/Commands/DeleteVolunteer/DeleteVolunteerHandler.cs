using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PetFamily.Application.Abstraction;
using PetFamily.Application.Database;
using PetFamily.Application.Extensions;
using PetFamily.Application.PetsManagment.Queries;
using PetFamily.Domain.Modules.Volunteers;
using PetFamily.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Application.Volunteers.WriteHandler.DeleteVolunteer;
public class DeleteVolunteerHandler : ICommandHandler<Guid, DeleteVolunteerCommand>
{
    private readonly IWriteVolunteersRepository _volunteersRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<DeleteVolunteerHandler> _logger;
    private readonly IValidator<DeleteVolunteerCommand> _validator;

    public DeleteVolunteerHandler(
        IWriteVolunteersRepository volunteersRepository,
        IUnitOfWork unitOfWork,
        ILogger<DeleteVolunteerHandler> logger,
        IValidator<DeleteVolunteerCommand> validator)
    {
        _volunteersRepository = volunteersRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
        _validator = validator;
    }

    public async Task<Result<Guid, ErrorList>> Handle(
        DeleteVolunteerCommand command, CancellationToken cancellationToken = default)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);

        if (validationResult.IsValid == false)
            return validationResult.ToErrorList();

        var volunteerResult = await _volunteersRepository.GetById(VolunteerId.Create(command.VolunteerId), cancellationToken);
        if (volunteerResult.IsFailure)
            return volunteerResult.Error.ToErrorList();

        var result = _volunteersRepository.Delete(volunteerResult.Value, cancellationToken);

        await _unitOfWork.SaveChanges(cancellationToken);

        _logger.LogInformation("Updates deleted with id {moduleId}", volunteerResult);

        return result;
    }
}

