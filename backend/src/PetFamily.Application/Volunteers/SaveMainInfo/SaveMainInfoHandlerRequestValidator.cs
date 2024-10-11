using FluentValidation;
using PetFamily.Application.Validation;
using PetFamily.Domain.Modules.Volunteers;
using PetFamily.Domain.Shared;

namespace PetFamily.Application.Volunteers.UpdateMainInfo;

public class SaveMainInfoHandlerRequestValidator : AbstractValidator<SaveMainInfoRequest>
{
    public SaveMainInfoHandlerRequestValidator()
    {
        RuleFor(r => r.VolunteerId).NotEmpty().WithError(Errors.General.ValueIsRequired());
    }
}

public class SaveMainInfoHandlerRequestDtoValidator : AbstractValidator<SaveMainInfoRequest>
{
    public SaveMainInfoHandlerRequestDtoValidator()
    {
        RuleFor(c => c.Dto.FullName).MustBeValueObject(x => FullName.Create(x.FirstName, x.LastName, x.Surname));
        RuleFor(c => c.Dto.PhoneNumber).MustBeValueObject(PhoneNumber.Create);
        RuleFor(c => c.Dto.Description).MustBeValueObject(Description.Create);
        RuleFor(c => c.Dto.Experience).MustBeValueObject(Experience.Create);
    }
}
