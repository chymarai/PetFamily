using Microsoft.AspNetCore.Mvc;
using PetFamily.API.Controllers.Species.Request;
using PetFamily.API.Extensions;
using PetFamily.Application.Specieses.Create;

namespace PetFamily.API.Controllers.Species;

public class SpeciesesController : ApplicationController
{
    [HttpPost]
    public async Task<ActionResult> Create(
        [FromServices] CreateSpeciesHandler handler,
        [FromBody] CreateSpeciesRequest request,
        CancellationToken token = default)
    {
        var result = await handler.Handle(request.ToCommand(), token);

        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }
}
