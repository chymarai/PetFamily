using FluentValidation;
using PetFamily.Application.Validation;
using PetFamily.Domain.Modules.Volunteers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Application.Volunteers.UpdateSocialNetwork;
public class UpdateSocialNetworkRequestValidator : AbstractValidator<UpdateSocialNetworkRequest>
{
    public UpdateSocialNetworkRequestValidator()
    {
        RuleForEach(c => c.Dto.SocialNetworkDetails.SocialNetwork)
           .MustBeValueObject(r => SocialNetwork.Create(r.Name, r.Url));
    }
}
