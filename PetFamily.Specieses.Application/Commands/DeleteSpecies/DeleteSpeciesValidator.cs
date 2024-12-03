using FluentValidation;

namespace PetFamily.Specieses.Application.Commands.DeleteSpecies;
public class DeleteSpeciesValidator : AbstractValidator<DeleteSpeciesCommand>
{
    public DeleteSpeciesValidator()
    {
        RuleFor(d => d.SpeciesId).NotEmpty();
    }
}
