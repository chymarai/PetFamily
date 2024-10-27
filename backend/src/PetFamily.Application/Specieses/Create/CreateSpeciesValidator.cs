using FluentValidation;
using PetFamily.Application.Validation;
using PetFamily.Domain.PetsManagment.ValueObjects.Pets;
using PetFamily.Domain.PetsManagment.ValueObjects.Shared;
using PetFamily.Domain.SpeciesManagment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Application.Specieses.Create;

public class CreateSpeciesValidator : AbstractValidator<CreateSpeciesCommand>
{
    public CreateSpeciesValidator()
    {
        RuleFor(c => c.SpeciesName).MustBeValueObject(SpeciesName.Create);
    }
}