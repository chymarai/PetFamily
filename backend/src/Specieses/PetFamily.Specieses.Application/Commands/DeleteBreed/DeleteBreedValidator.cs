using FluentValidation;

namespace PetFamily.Specieses.Application.Commands.DeleteBreed;
public class DeleteBreedValidator : AbstractValidator<DeleteBreedCommand>
{
    public DeleteBreedValidator()
    {
        RuleFor(b => b.BreedId).NotEmpty();
        RuleFor(b => b.SpeciesId).NotEmpty();
    }
}
