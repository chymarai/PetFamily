using CSharpFunctionalExtensions;
using FluentValidation;
using PetFamily.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Application.Validation;
public static class CustomValidators
{
    //расширяем кастомный вадидатор

    public static IRuleBuilderOptionsConditions<T, TElement> MustBeValueObject<T, TElement, TValueObject>(
        this IRuleBuilder<T, TElement> ruleBuilder,
        Func<TElement, Result<TValueObject, Error>> factoryMethod)
    {
        return ruleBuilder.Custom((value, context) =>
        {
            Result<TValueObject, Error> result = factoryMethod(value);

            if (result.IsSuccess)
                return;

            context.AddFailure(result.Error.Serialize());
        });
    }

    public static IRuleBuilderOptionsConditions<T, string> MustBeEnum<T>(
       this IRuleBuilder<T, string> ruleBuilder,
       Type enumType)
    {
        return ruleBuilder.Custom((value, context) =>
        {
            if (!Enum.TryParse(enumType, value, true, out _))
                context.AddFailure(Error.Failure(
                        enumType.Name,
                        "Invalid enum")
                    .Serialize());
        });
    }

    public static IRuleBuilderOptions<T, TElement> WithError<T, TElement>(
       this IRuleBuilderOptions<T, TElement> ruleBuilder,
       Error error)
       => ruleBuilder.WithMessage(error.Serialize());
}
