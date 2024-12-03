using FluentValidation;

namespace PetFamily.Volunteers.Application.Commands.SoftDeletePet;

public class SoftDeletePetValidator : AbstractValidator<SoftDeletePetCommand>
{
    public SoftDeletePetValidator()
    {
        RuleFor(d => d.VolunteerId).NotEmpty();
        RuleFor(d => d.PetId).NotEmpty();
    }
}