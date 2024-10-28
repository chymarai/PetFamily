using PetFamily.Application.DTOs;
using PetFamily.Application.Pet.AddFiles;

namespace PetFamily.API.Prosessors;

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
