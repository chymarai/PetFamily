using FluentValidation;
using PetFamily.Application.Volunteers.WriteHandler.DeleteVolunteer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Application.BreedsManagment.DeleteSpecies;
public class DeleteSpeciesValidator : AbstractValidator<DeleteSpeciesCommand>
{
    public DeleteSpeciesValidator()
    {
        RuleFor(d => d.SpeciesId).NotEmpty();
    }
}
