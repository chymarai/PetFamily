using FluentValidation;

namespace PetFamily.Volunteers.Application.Commands.HardDeletePet;

public class HardDeletePetValidator : AbstractValidator<HardDeletePetCommand>
{
    public HardDeletePetValidator()
    {
        RuleFor(d => d.VolunteerId).NotEmpty();
        RuleFor(d => d.PetId).NotEmpty();
    }
}