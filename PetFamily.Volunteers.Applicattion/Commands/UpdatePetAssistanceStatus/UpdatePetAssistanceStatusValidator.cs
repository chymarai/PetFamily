using FluentValidation;
using PetFamily.Core.Validation;
using PetFamily.Volunteers.Domain.PetsValueObjects;

namespace PetFamily.Volunteers.Application.Commands.UpdatePetAssistanceStatus;
public class UpdatePetAssistanceStatusValidator : AbstractValidator<UpdatePetAssistanceStatusCommand>
{
    public UpdatePetAssistanceStatusValidator()
    {
        RuleFor(d => d.VolunteerId).NotEmpty();
        RuleFor(d => d.PetId).NotEmpty();
        RuleFor(p => p.AssistanceStatus).MustBeEnum(typeof(AssistanceStatus));
    }
}
