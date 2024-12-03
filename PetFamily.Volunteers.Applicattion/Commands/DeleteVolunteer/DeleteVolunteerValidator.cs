using FluentValidation;

namespace PetFamily.Volunteers.Application.Commands.DeleteVolunteer;
public class DeleteVolunteerValidator : AbstractValidator<DeleteVolunteerCommand>
{
    public DeleteVolunteerValidator()
    {
        RuleFor(d => d.VolunteerId).NotEmpty();
    }
}
