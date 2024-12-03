using FluentValidation;
using PetFamily.Core.Validation;
using PetFamily.Specieses.Domain;

namespace PetFamily.Specieses.Application.Commands.CreateSpecies;

public class CreateSpeciesValidator : AbstractValidator<CreateSpeciesCommand>
{
    public CreateSpeciesValidator()
    {
        RuleFor(c => c.SpeciesName).MustBeValueObject(SpeciesName.Create);
    }
}