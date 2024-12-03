using FluentValidation;
using PetFamily.Core.Validation;
using PetFamily.Volunteers.Domain.VolunteersValueObjects;

namespace PetFamily.Volunteers.Application.Commands.UpdateSocialNetwork;
public class UpdateSocialNetworkCommandValidator : AbstractValidator<UpdateSocialNetworkCommand>
{
    public UpdateSocialNetworkCommandValidator()
    {
        RuleForEach(c => c.SocialNetworkDetails.SocialNetwork)
           .MustBeValueObject(r => SocialNetwork.Create(r.Name, r.Url));
    }
}
