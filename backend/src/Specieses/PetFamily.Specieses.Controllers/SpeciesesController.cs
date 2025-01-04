using Microsoft.AspNetCore.Mvc;
using PetFamily.Framework;
using PetFamily.Framework.Authorization;
using PetFamily.Specieses.Application.Commands.CreateBreed;
using PetFamily.Specieses.Application.Commands.CreateSpecies;
using PetFamily.Specieses.Application.Commands.DeleteBreed;
using PetFamily.Specieses.Application.Commands.DeleteSpecies;
using PetFamily.Specieses.Application.Queries.GetBreeds;
using PetFamily.Specieses.Application.Queries.GetSpecieses;
using PetFamily.Specieses.Contracts.Requests;

namespace PetFamily.Specieses.Presentation;

public class SpeciesesController : ApplicationController
{
    [Permission(Permissions.Specieses.GetSpecies)]
    [HttpGet]
    public async Task<ActionResult> GetSpesiesesOrderByName(
        [FromServices] GetSpeciesesOrderByNameHandler handler,
        CancellationToken token = default)
    {
        var result = await handler.Handle(token);

        return Ok(result);
    }

    [Permission(Permissions.Breeds.GetBreed)]
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

    [Permission(Permissions.Specieses.CreateSpecies)]
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

    [Permission(Permissions.Breeds.CreateBreed)]
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

    [Permission(Permissions.Specieses.DeleteSpecies)]
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

    [Permission(Permissions.Breeds.DeleteBreed)]
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
