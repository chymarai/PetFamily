using FluentValidation;
using PetFamily.Application.Validation;
using PetFamily.Domain.Modules.Volunteers;


namespace PetFamily.Application.Volunteers.CreateVolunteer;

public class CreateVolunteerRequestValidator : AbstractValidator<CreateVolunteerRequest> //валидация входных данных с помощью библиотеки FluentValidation
{
    public CreateVolunteerRequestValidator() //правила для валидации
    {
        RuleFor(c => c.Email).MustBeValueObject(Email.Create);
        RuleFor(c => c.PhoneNumber).MustBeValueObject(PhoneNumber.Create);
        RuleFor(c => c.Description).MustBeValueObject(Description.Create);
        RuleFor(c => c.FullName).MustBeValueObject(x => FullName.Create(x.FirstName, x.LastName, x.Surname));
    }
}
