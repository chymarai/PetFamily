using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PetFamily.Accounts.Application.Commands.LoginUser;
using PetFamily.Accounts.Application.Commands.RegisterUser;
using PetFamily.Accounts.Infrastructure;
using PetFamily.Accounts.Presentation.Requests;
using PetFamily.Framework;
using PetFamily.Framework.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Accounts.Presentation;
public class AccountsController : ApplicationController
{
    [Permission("pet.get")]
    [HttpGet("test")]
    public ActionResult TestAdmin()
    {
        return Ok();
    }

    [HttpPost("registration")]
    public async Task<ActionResult> Register(
        [FromBody] RegisterUserRequest request,
        [FromServices] RegisterUserHandler handler, 
        CancellationToken token)
    {
        var result = await handler.Handle(request.ToCommand(), token);
        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok();
    }

    [HttpPost("login")]
    public async Task<ActionResult> Login(
    [FromBody] LoginUserRequest request,
    [FromServices] LoginUserHandler handler,
    CancellationToken token)
    {
        var result = await handler.Handle(request.ToCommand(), token);
        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }
}
