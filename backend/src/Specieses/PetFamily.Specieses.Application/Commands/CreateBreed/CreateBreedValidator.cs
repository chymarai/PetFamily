using FluentValidation;
using PetFamily.Core.Validation;
using PetFamily.Specieses.Domain;

namespace PetFamily.Specieses.Application.Commands.CreateBreed;
public class CreateBreedValidator : AbstractValidator<CreateBreedCommand>
{
    public CreateBreedValidator()
    {
        RuleFor(c => c.BreedName).MustBeValueObject(BreedName.Create);
    }
}
