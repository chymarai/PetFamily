using FluentValidation;
using PetFamily.Core.Validation;
using PetFamily.SharedKernel;

namespace PetFamily.Volunteers.Application.Commands.AddFiles;
public class UploadFilesToPetValidator : AbstractValidator<UploadFilesToPetCommand>
{
    public UploadFilesToPetValidator()
    {
        RuleFor(u => u.VolunteerId).NotEmpty().WithError(Errors.General.ValueIsRequired());
        RuleFor(u => u.PetId).NotEmpty().WithError(Errors.General.ValueIsRequired());
        RuleFor(u => u.Files).NotEmpty().WithError(Errors.General.ValueIsRequired());
    }
}
