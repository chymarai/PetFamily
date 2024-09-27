using PetFamily.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;

namespace PetFamily.Domain.Modules.Volunteers;

public record FullName
{
    private FullName(string lastName, string firstName, string surName )
    {
        LastName = lastName;
        FirstName = firstName;
        SurName = surName;
    }
    public string LastName { get; } = default!;
    public string FirstName { get; } = default!;
    public string SurName { get; } = default!;
   
    public static Result<FullName, Error> Create(string lastName, string firstName, string surName)
    {
        if (string.IsNullOrWhiteSpace(lastName) || lastName.Length > Constants.MAX_LOW_TEXT_LENGTH)
            return Errors.General.ValueIsInvalid("LastName");

        if (string.IsNullOrWhiteSpace(firstName) || firstName.Length > Constants.MAX_LOW_TEXT_LENGTH)
            return Errors.General.ValueIsInvalid("FirstName");

        if (string.IsNullOrWhiteSpace(surName) || surName.Length > Constants.MAX_LOW_TEXT_LENGTH)
            return Errors.General.ValueIsInvalid("SurName");

        return new FullName(lastName,firstName,surName);
    }
}
