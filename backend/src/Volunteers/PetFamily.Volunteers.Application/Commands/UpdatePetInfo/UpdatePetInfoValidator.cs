using FluentValidation;
using PetFamily.Core.Validation;
using PetFamily.SharedKernel.ValueObjects;
using PetFamily.Volunteers.Domain.PetsValueObjects;

namespace PetFamily.Volunteers.Application.Commands.UpdatePetInfo;

public class UpdatePetInfoValidator : AbstractValidator<UpdatePetInfoCommand>
{
    public UpdatePetInfoValidator()
    {
        RuleFor(d => d.VolunteerId).NotEmpty();
        RuleFor(d => d.PetId).NotEmpty();
        RuleFor(p => p.Description).MustBeValueObject(Description.Create);
        RuleFor(p => p.Color).MustBeValueObject(Color.Create);
        RuleFor(p => p.HealthInformation).MustBeValueObject(HealthInformation.Create);
        RuleFor(p => p.Address).MustBeValueObject(a => Address.Create(a.Country, a.Region, a.City, a.Street));
        RuleFor(p => p.Weight).MustBeValueObject(Weight.Create);
        RuleFor(p => p.PhoneNumber).MustBeValueObject(PhoneNumber.Create);
        RuleFor(p => p.AssistanceStatus).MustBeEnum(typeof(AssistanceStatus));
        RuleFor(p => p.BirthDate).MustBeValueObject(BirthDate.Create);
        RuleForEach(c => c.RequisiteDetails.Requisite)
            .MustBeValueObject(r => Requisite.Create(r.Name, r.Description));
    }
}