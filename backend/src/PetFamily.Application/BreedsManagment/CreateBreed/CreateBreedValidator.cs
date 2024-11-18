using FluentValidation;
using PetFamily.Application.Specieses.Create;
using PetFamily.Application.Validation;
using PetFamily.Domain.SpeciesManagment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Application.Specieses.CreateBreed;
public class CreateBreedValidator : AbstractValidator<CreateBreedCommand>
{
    public CreateBreedValidator()
    {
        RuleFor(c => c.BreedName).MustBeValueObject(BreedName.Create);
    }
}
