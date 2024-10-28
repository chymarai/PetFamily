using FluentValidation;
using PetFamily.Application.Validation;
using PetFamily.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Application.Pet.AddFiles;
public class UploadFilesToPetValidator : AbstractValidator<UploadFilesToPetCommand>
{
    public UploadFilesToPetValidator()
    {
        RuleFor(u => u.VolunteerId).NotEmpty().WithError(Errors.General.ValueIsRequired());
        RuleFor(u => u.PetId).NotEmpty().WithError(Errors.General.ValueIsRequired());
        RuleFor(u => u.Files).NotEmpty().WithError(Errors.General.ValueIsRequired());
    }
}
