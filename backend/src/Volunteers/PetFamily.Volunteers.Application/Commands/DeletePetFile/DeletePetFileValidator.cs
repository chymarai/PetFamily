using FluentValidation;

namespace PetFamily.Volunteers.Application.Commands.DeletePetFile;

public class DeletePetFileValidator : AbstractValidator<DeletePetFileCommand>
{
    public DeletePetFileValidator()
    {
        RuleFor(d => d.VolunteerId).NotEmpty();
        RuleFor(d => d.PetId).NotEmpty();
        RuleFor(d => d.FilePath).NotEmpty();
    }
}