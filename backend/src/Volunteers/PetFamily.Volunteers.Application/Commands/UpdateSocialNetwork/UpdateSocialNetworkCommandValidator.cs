using FluentValidation;
using PetFamily.Core.Validation;
using PetFamily.SharedKernel.ValueObjects;

namespace PetFamily.Volunteers.Application.Commands.UpdateSocialNetwork;
public class UpdateSocialNetworkCommandValidator : AbstractValidator<UpdateSocialNetworkCommand>
{
    public UpdateSocialNetworkCommandValidator()
    {
        RuleForEach(c => c.SocialNetworkDetails.SocialNetwork)
           .MustBeValueObject(r => SocialNetwork.Create(r.Name, r.Url));
    }
}
