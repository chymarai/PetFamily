using FluentValidation;
using PetFamily.Application.Validation;
using PetFamily.Domain.PetsManagment.ValueObjects.Volunteers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Application.Volunteers.UpdateSocialNetwork;
public class UpdateSocialNetworkCommandValidator : AbstractValidator<UpdateSocialNetworkCommand>
{
    public UpdateSocialNetworkCommandValidator()
    {
        RuleForEach(c => c.SocialNetworkDetails.SocialNetwork)
           .MustBeValueObject(r => SocialNetwork.Create(r.Name, r.Url));
    }
}
