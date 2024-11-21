using Microsoft.AspNetCore.Mvc;
using PetFamily.API.Controllers.Species.Request;
using PetFamily.API.Extensions;
using PetFamily.Application.BreedsManagment.DeleteBreed;
using PetFamily.Application.BreedsManagment.DeleteSpecies;
using PetFamily.Application.Specieses.Create;
using PetFamily.Application.Specieses.CreateBreed;

namespace PetFamily.API.Controllers.Species;

public class SpeciesesController : ApplicationController
{
    [HttpGet("{id:guid}/breed")]
    public async Task<ActionResult> GetBreedsOrderByName(
       [FromRoute] Guid id,
       [FromServices] GetBreedsOrderByNameHandler handler,
       CancellationToken token = default)
    {
        var query = new GetBreedsOrderByNameQuery(id);

        var result = await handler.Handle(query, token);

        return Ok(result);
    }
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

    [HttpDelete("{id:guid}/delete")]
    public async Task<ActionResult> DeleteSpecies(
        [FromRoute] Guid id,
        [FromServices] DeleteSpeciesHandler handler, 
        CancellationToken token)
    {
        var result = await handler.Handle(new DeleteSpeciesCommand(id), token);

        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }

    [HttpDelete("{speciesId:guid}/{breedId:guid}/delete")]
    public async Task<ActionResult> DeleteBreed(
    [FromRoute] Guid speciesId,
    [FromRoute] Guid breedId,
    [FromServices] DeleteBreedHandler handler,
    CancellationToken token)
    {
        var result = await handler.Handle(new DeleteBreedCommand(speciesId, breedId), token);

        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }
}
