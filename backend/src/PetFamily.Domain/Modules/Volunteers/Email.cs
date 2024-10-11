using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PetFamily.Domain.Modules.Volunteers;

public record Email
{
    private static readonly string EmailRegex = @"^[\w-\.]{1,40}@([\w-]+\.)+[\w-]{2,4}$";

    private Email(string value)
    {
        Value = value;
    }
    public string Value { get; } = default!;

    public static Result<Email, Shared.Error> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || value.Length > Constants.MAX_LOW_TEXT_LENGTH/* || Regex.IsMatch(value, EmailRegex) == false*/)
            return Errors.General.ValueIsInvalid("Email");

        return new Email(value);
    }
}

