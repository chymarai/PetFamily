using FluentValidation;
using PetFamily.Application.Validation;
using PetFamily.Application.Volunteers.UpdateMainInfo;
using PetFamily.Domain.Modules.Volunteers;
using PetFamily.Domain.Shared;

namespace PetFamily.Application.Volunteers.UpdateMainInfo;

public class UpdateMainInfoRequestValidator : AbstractValidator<UpdateMainInfoRequest>
{
    public UpdateMainInfoRequestValidator()
    {
        RuleFor(r => r.VolunteerId).NotEmpty().WithError(Errors.General.ValueIsRequired());
    }
}

public class UpdateMainInfoRequestDtoValidator : AbstractValidator<UpdateMainInfoRequest>
{
    public UpdateMainInfoRequestDtoValidator()
    {
        RuleFor(c => c.Dto.FullName).MustBeValueObject(x => FullName.Create(x.FirstName, x.LastName, x.Surname));
        RuleFor(c => c.Dto.PhoneNumber).MustBeValueObject(PhoneNumber.Create);
        RuleFor(c => c.Dto.Description).MustBeValueObject(Description.Create);
        RuleFor(c => c.Dto.Experience).MustBeValueObject(Experience.Create);
        RuleForEach(c => c.Dto.RequisiteDetails.Requisite)
            .MustBeValueObject(r => Requisite.Create(r.Name, r.Description));
    }
}
