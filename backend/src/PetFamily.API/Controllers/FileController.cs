using Microsoft.AspNetCore.Mvc;
using PetFamily.API.Extensions;
using PetFamily.Application.File;
using PetFamily.Application.FileProvider;

namespace PetFamily.API.Controllers;

public class FileController : ApplicationController
{
    [HttpPost]
    public async Task<ActionResult> Add(
        IFormFile file,
        [FromServices] AddFileHandler handler,
        CancellationToken cancellationToken = default)
    {
        await using var stream = file.OpenReadStream();

        var path = Guid.NewGuid().ToString();

        var fileData = new AddFileRequest(stream, "photos", path);

        var result = await handler.Handle(fileData, cancellationToken);

        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }
}
