using FluentValidation;
using PetFamily.Core.Validation;
using PetFamily.SharedKernel;
using PetFamily.SharedKernel.ValueObjects;
using PetFamily.Volunteers.Domain.VolunteersValueObjects;

namespace PetFamily.Volunteers.Application.Commands.UpdateMainInfos;

public class UpdateMainInfoValidator : AbstractValidator<UpdateMainInfoCommand>
{
    public UpdateMainInfoValidator()
    {
        RuleFor(r => r.VolunteerId).NotEmpty().WithError(Errors.General.ValueIsRequired());
        RuleFor(c => c.FullName).MustBeValueObject(x => FullName.Create(x.FirstName, x.LastName, x.Surname));
        RuleFor(c => c.PhoneNumber).MustBeValueObject(PhoneNumber.Create);
        RuleFor(c => c.Description).MustBeValueObject(Description.Create);
        RuleFor(c => c.Experience).MustBeValueObject(Experience.Create);
    }
}
