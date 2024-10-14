using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Application.Volunteers.DeleteVolunteer;
public class DeleteVolunteerValidator : AbstractValidator<DeleteVolunteerRequest>
{
    public DeleteVolunteerValidator()
    {
        RuleFor(d => d.VolunteerId).NotEmpty();
    }
}
