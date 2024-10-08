﻿using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using PetFamily.API.Response;
using PetFamily.Domain.Shared;
using System.ComponentModel.DataAnnotations;
using ValidationResult = FluentValidation.Results.ValidationResult;

namespace PetFamily.API.Extensions;

public static class ResponseExtensions
{
    public static ActionResult ToResponse(this Error error)
    {
        var statusCode = error.Type switch
        {
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            ErrorType.Failure => StatusCodes.Status500InternalServerError,
            _ => StatusCodes.Status500InternalServerError
        };

        var responseError = new ResponseError(error.Code, error.Message, null);

        var envelope = Envelope.Error([responseError]);

        return new ObjectResult(envelope)
        {
            StatusCode = statusCode
        };
    }

    public static ActionResult ToValidationErrorResponse(this ValidationResult result) //метод расширения
    {
        if (result.IsValid)
            throw new InvalidOperationException("Result can not be succeed");

        var validationErrors = result.Errors;

        List<ResponseError> errors = [];

        foreach (var validationError in validationErrors)
        {
            var errorMessage = validationError.ErrorMessage;

            var error = Error.Deserialize(errorMessage);

            var responseError = new ResponseError(
                error.Code,
                error.Message,
                validationError.PropertyName);

            errors.Add(responseError);
        }

        var envelope = Envelope.Error(errors);

        return new ObjectResult(envelope)
        {
            StatusCode = StatusCodes.Status400BadRequest
        };
    }
}
