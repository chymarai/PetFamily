using FluentValidation.Results;
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
        var statusCode = GetStatusCodeForErrorType(error.Type);

        var envelope = Envelope.Error(error.ToErrorList());

        return new ObjectResult(envelope)
        {
            StatusCode = statusCode
        };
    }

    public static ActionResult ToResponse(this ErrorList errors) //получаем список ошибок
    {
        if (!errors.Any()) //нет ошибок
        {
            return new ObjectResult(null)
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };
        }

        var distinctErrorTypes = errors
            .Select(x => x.Type)
            .Distinct() //отбираем только уникальные ошибки
            .ToList();

        var statusCodes = distinctErrorTypes.Count > 1
            ? StatusCodes.Status500InternalServerError //если ошибок >1
            : GetStatusCodeForErrorType(distinctErrorTypes.First()); //если все ошибки одного типа

        var envelope = Envelope.Error(errors);

        return new ObjectResult(envelope)
        {
            StatusCode = statusCodes
        };
    }

    private static int GetStatusCodeForErrorType(ErrorType errorType) =>
        errorType switch
        {
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            ErrorType.Failure => StatusCodes.Status500InternalServerError,
            _ => StatusCodes.Status500InternalServerError
        };
}
