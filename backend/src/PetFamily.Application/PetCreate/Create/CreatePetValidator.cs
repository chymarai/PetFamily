using FluentValidation;
using PetFamily.Application.Specieses.Create;
using PetFamily.Application.Validation;
using PetFamily.Domain.PetsManagment.ValueObjects.Pets;
using PetFamily.Domain.PetsManagment.ValueObjects.Shared;
using PetFamily.Domain.SpeciesManagment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Application.Pet.Create;
public class CreatePetValidator : AbstractValidator<CreatePetCommand>
{
    public CreatePetValidator()
    {
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