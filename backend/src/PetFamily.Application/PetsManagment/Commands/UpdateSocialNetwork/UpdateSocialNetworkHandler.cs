using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Org.BouncyCastle.Asn1.Ocsp;
using PetFamily.Application.Abstraction;
using PetFamily.Application.Database;
using PetFamily.Application.Extensions;
using PetFamily.Application.PetsManagment.Queries;
using PetFamily.Domain.Modules.Volunteers;
using PetFamily.Domain.PetsManagment.ValueObjects.Volunteers;
using PetFamily.Domain.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Application.Volunteers.WriteHandler.UpdateSocialNetwork;
public class UpdateSocialNetworkHandler : ICommandHandler<Guid, UpdateSocialNetworkCommand>
{
    private readonly IWriteVolunteersRepository _volunteersRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<UpdateSocialNetworkHandler> _logger;
    private readonly IValidator<UpdateSocialNetworkCommand> _validator;

    public UpdateSocialNetworkHandler(
        IWriteVolunteersRepository volunteersRepository,
        IUnitOfWork unitOfWork,
        ILogger<UpdateSocialNetworkHandler> logger,
        IValidator<UpdateSocialNetworkCommand> validator)
    {
        _volunteersRepository = volunteersRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
        _validator = validator;
    }

    public async Task<Result<Guid, ErrorList>> Handle(
        UpdateSocialNetworkCommand command,
        CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (validationResult.IsValid == false)
            return validationResult.ToErrorList();

        var volunteerResult = await _volunteersRepository.GetById(VolunteerId.Create(command.VolunteerId), cancellationToken);
        if (volunteerResult.IsFailure)
            return volunteerResult.Error.ToErrorList();

        var socialNetworkDetails = command.SocialNetworkDetails.SocialNetwork
            .Select(s => SocialNetwork.Create(s.Name, s.Url).Value);

        volunteerResult.Value.UpdateSocialNetwork(new SocialNetworkDetails(socialNetworkDetails));

        await _unitOfWork.SaveChanges(cancellationToken);

        _logger.LogInformation("Update {socialNetwork} with id {volunteerId}", socialNetworkDetails, volunteerResult.Value);

        return volunteerResult.Value.Id.Value;
    }

}
