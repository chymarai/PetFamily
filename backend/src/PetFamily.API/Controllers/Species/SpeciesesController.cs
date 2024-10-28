using Microsoft.AspNetCore.Mvc;
using PetFamily.API.Controllers.Species.Request;
using PetFamily.API.Extensions;
using PetFamily.Application.Specieses.Create;
using PetFamily.Application.Specieses.CreateBreed;

namespace PetFamily.API.Controllers.Species;

public class SpeciesesController : ApplicationController
{
    [HttpPost]
    public async Task<ActionResult> Create(
        [FromBody] CreateSpeciesRequest request,
        [FromServices] CreateSpeciesHandler handler,
        CancellationToken token = default)
    {
        var result = await handler.Handle(request.ToCommand(), token);

        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }

    [HttpPost("{id:guid}/breed")]
    public async Task<ActionResult> CreateBreed(
        [FromRoute] Guid id,
        [FromBody] CreateBreedRequest request,
        [FromServices] CreateBreedHandler handler,
        CancellationToken token)
    {
        var result = await handler.Handle(request.ToCommand(id), token);

        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }
}
