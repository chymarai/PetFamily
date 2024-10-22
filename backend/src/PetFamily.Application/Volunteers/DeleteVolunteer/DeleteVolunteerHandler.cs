using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using PetFamily.Application.Volunteers.DeleteVolunteer;
using PetFamily.Domain.Modules.Volunteers;
using PetFamily.Domain.Shared;
using PetFamily.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Application.Volunteers.Delete;
public class DeleteVolunteerHandler
{
    public readonly IVolunteersRepository _volunteersRepository;
    public readonly ILogger<DeleteVolunteerHandler> _logger;

    public DeleteVolunteerHandler(
        IVolunteersRepository volunteersRepository,
        ILogger<DeleteVolunteerHandler> logger)
    {
        _volunteersRepository = volunteersRepository;
        _logger = logger;
    }

    public async Task<Result<Guid, Error>> Handle(
        DeleteVolunteerCommand command, CancellationToken cancellationToken = default)
    {
        var volunteerResult = await _volunteersRepository.GetById(VolunteerId.Create(command.VolunteerId), cancellationToken);
        if (volunteerResult.IsFailure)
            return volunteerResult.Error;

        var result = _volunteersRepository.Delete(volunteerResult.Value, cancellationToken);

        _logger.LogInformation("Updates deleted with id {moduleId}", volunteerResult);

        return result;
    }
}

