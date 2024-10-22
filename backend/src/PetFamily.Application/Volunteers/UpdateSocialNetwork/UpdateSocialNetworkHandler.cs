using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using Org.BouncyCastle.Asn1.Ocsp;
using PetFamily.Domain.Modules.Volunteers;
using PetFamily.Domain.PetsManagment.ValueObjects.Volunteers;
using PetFamily.Domain.Shared;
using PetFamily.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Application.Volunteers.UpdateSocialNetwork;
public class UpdateSocialNetworkHandler
{
    public readonly IVolunteersRepository _volunteersRepository;
    public readonly ILogger<UpdateSocialNetworkHandler> _logger;

    public UpdateSocialNetworkHandler(
        IVolunteersRepository volunteersRepository,
        ILogger<UpdateSocialNetworkHandler> logger)
    {
        _volunteersRepository = volunteersRepository;
        _logger = logger;
    }

    public async Task<Result<Guid, Error>> Handle(
        UpdateSocialNetworkCommand command,
        CancellationToken cancellationToken)
    {
        var volunteerResult = await _volunteersRepository.GetById(VolunteerId.Create(command.VolunteerId), cancellationToken);
        if (volunteerResult.IsFailure)
            return Errors.General.NotFound();

        var socialNetworkDetailsDto = command.Dto.SocialNetworkDetails;
        var socialNetworkDetails = SocialNetworkDetails.Create(socialNetworkDetailsDto.SocialNetwork
            .Select(s => SocialNetwork.Create(s.Name, s.Url).Value));

        volunteerResult.Value.UpdateSocialNetwork(socialNetworkDetails);

        var result = _volunteersRepository.Save(volunteerResult.Value, cancellationToken);

        _logger.LogInformation("Update {socialNetwork}", socialNetworkDetails);

        return result;
    }

}
