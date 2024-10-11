using FluentValidation;
using PetFamily.Application.Validation;
using PetFamily.Application.Volunteers.UpdateMainInfo;
using PetFamily.Domain.Modules.Volunteers;
using PetFamily.Domain.Shared;

namespace PetFamily.Application.Volunteers.UpdateMainInfo;

public class UpdateMainInfoHandlerRequestValidator : AbstractValidator<UpdateMainInfoRequest>
{
    public UpdateMainInfoHandlerRequestValidator()
    {
        RuleFor(r => r.VolunteerId).NotEmpty().WithError(Errors.General.ValueIsRequired());
    }
}

public class UpdateMainInfoHandlerRequestDtoValidator : AbstractValidator<UpdateMainInfoRequest>
{
    public UpdateMainInfoHandlerRequestDtoValidator()
    {
        RuleFor(c => c.Dto.FullName).MustBeValueObject(x => FullName.Create(x.FirstName, x.LastName, x.Surname));
        RuleFor(c => c.Dto.PhoneNumber).MustBeValueObject(PhoneNumber.Create);
        RuleFor(c => c.Dto.Description).MustBeValueObject(Description.Create);
        RuleFor(c => c.Dto.Experience).MustBeValueObject(Experience.Create);
    }
}
