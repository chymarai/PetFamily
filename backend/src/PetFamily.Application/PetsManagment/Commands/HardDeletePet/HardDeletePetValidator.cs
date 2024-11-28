using FluentValidation;
using PetFamily.Application.Volunteers.WriteHandler.DeleteVolunteer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Application.PetsManagment.Commands.HardDeletePet;

public class HardDeletePetValidator : AbstractValidator<HardDeletePetCommand>
{
    public HardDeletePetValidator()
    {
        RuleFor(d => d.VolunteerId).NotEmpty();
        RuleFor(d => d.PetId).NotEmpty();
    }
}