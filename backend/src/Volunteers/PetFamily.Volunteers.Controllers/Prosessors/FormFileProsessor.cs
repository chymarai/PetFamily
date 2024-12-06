using Microsoft.AspNetCore.Http;
using PetFamily.Core.DTOs;

namespace PetFamily.Web.Prosessors;

public class FormFileProsessor : IAsyncDisposable
{
    private readonly List<UploadFileDto> _fileDtos = [];

    public List<UploadFileDto> Process(IFormFileCollection files)
    {
        foreach (var file in files)
        {
            var stream = file.OpenReadStream();

            _fileDtos.Add(new UploadFileDto(stream, file.FileName));
        }

        return _fileDtos;
    }
    public async ValueTask DisposeAsync()
    {
        foreach (var file in _fileDtos)
        {
            await file.Stream.DisposeAsync();
        }
    }
}
