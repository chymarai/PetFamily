using FluentValidation;
using PetFamily.Application.Volunteers.WriteHandler.DeleteVolunteer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Application.PetsManagment.Commands.SoftDeletePet;

public class SoftDeletePetValidator : AbstractValidator<SoftDeletePetCommand>
{
    public SoftDeletePetValidator()
    {
        RuleFor(d => d.VolunteerId).NotEmpty();
    }
}