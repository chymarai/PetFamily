using FluentValidation;
using PetFamily.Application.PetsManagment.Commands.SoftDeletePet;
using PetFamily.Application.PetsManagment.Commands.UpdatePetFiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Application.PetsManagment.Commands.RemovePetFiles;

public class DeletePetFileValidator : AbstractValidator<DeletePetFileCommand>
{
    public DeletePetFileValidator()
    {
        RuleFor(d => d.VolunteerId).NotEmpty();
        RuleFor(d => d.PetId).NotEmpty();
        RuleFor(d => d.FilePath).NotEmpty();
    }
}