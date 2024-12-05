using FluentValidation;
using PetFamily.Core.Validation;
using PetFamily.SharedKernel.ValueObjects;
using PetFamily.Volunteers.Domain.VolunteersValueObjects;


namespace PetFamily.Volunteers.Application.Commands.Create;

public class CreateVolunteerRequestValidator : AbstractValidator<CreateVolunteerCommand> //валидация входных данных с помощью библиотеки FluentValidation
{
    public CreateVolunteerRequestValidator() //правила для валидации
    {
        RuleFor(c => c.FullName).MustBeValueObject(x => FullName.Create(x.FirstName, x.LastName, x.Surname));
        RuleFor(c => c.Email).MustBeValueObject(Email.Create);
        RuleFor(c => c.PhoneNumber).MustBeValueObject(PhoneNumber.Create);
        RuleFor(c => c.Description).MustBeValueObject(Description.Create);
        RuleFor(c => c.Experience).MustBeValueObject(Experience.Create);
        RuleForEach(c => c.SocialNetworkDetails.SocialNetwork)
            .MustBeValueObject(r => SocialNetwork.Create(r.Name, r.Url));
        RuleForEach(c => c.RequisiteDetails.Requisite)
            .MustBeValueObject(r => Requisite.Create(r.Name, r.Description));
    }
}
