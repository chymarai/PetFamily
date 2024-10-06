using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PetFamily.API.Response;
using PetFamily.Domain.Shared;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Results;
using System.Collections.Generic;

namespace PetFamily.API.Validation;

public class CustomResultFactory : IFluentValidationAutoValidationResultFactory
{
    public IActionResult CreateActionResult(ActionExecutingContext context, ValidationProblemDetails? validationProblemDetails)
    {
        if (validationProblemDetails is null)
        {
            throw new InvalidOperationException("ValidationProblemDetails is null");
        }

        List<ResponseError> errors = [];

        foreach (var (invalidField, validationErrors) in validationProblemDetails.Errors)
        {

            foreach (var errorMessage in validationErrors)
            {
                var error = Error.Deserialize(errorMessage);

                var responseError = new ResponseError(
                    error.Code,
                    error.Message,
                    invalidField);

                errors.Add(responseError);
            }
            
        }

        var envelope = Envelope.Error(errors);

        return new ObjectResult(envelope)
        {
            StatusCode = StatusCodes.Status400BadRequest
        };
    }
}
