using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using NPOI.SS.Formula.Functions;
using PetFamily.API.Response;

namespace PetFamily.API.Controllers;

    [ApiController]
    [Route("api/[controller]")]
public class ApplicationController : ControllerBase
{
    public override OkObjectResult Ok(object? value)
    {
        var envelope = Envelope.Ok(value);

        return base.Ok(envelope);
    }

}
