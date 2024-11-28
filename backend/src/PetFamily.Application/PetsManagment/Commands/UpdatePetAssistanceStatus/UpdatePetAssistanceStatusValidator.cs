using FluentValidation;
using PetFamily.Application.PetsManagment.Commands.UpdatePetInfo;
using PetFamily.Application.Validation;
using PetFamily.Domain.PetsManagment.ValueObjects.Pets;
using PetFamily.Domain.PetsManagment.ValueObjects.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Application.PetsManagment.Commands.UpdatePetAssistanceStatus;
public class UpdatePetAssistanceStatusValidator : AbstractValidator<UpdatePetAssistanceStatusCommand>
{
    public UpdatePetAssistanceStatusValidator()
    {
        RuleFor(d => d.VolunteerId).NotEmpty();
        RuleFor(d => d.PetId).NotEmpty();
        RuleFor(p => p.AssistanceStatus).MustBeEnum(typeof(AssistanceStatus));
    }
}
