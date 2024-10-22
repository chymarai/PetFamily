
using Org.BouncyCastle.Asn1.Ocsp;
using PetFamily.Application.DTOs;
using PetFamily.Application.Pet.Create;

namespace PetFamily.API.Prosessors;

public class FormFileProsessor : IAsyncDisposable
{
    List<CreateFileCommand> _fileDto = [];

    public List<CreateFileCommand> Process(IFormFileCollection files)
    {
        foreach (var file in files)
        {
            var stream = file.OpenReadStream();

            _fileDto.Add(new CreateFileCommand(stream, file.FileName));
        }

        return _fileDto;
    }
    public async ValueTask DisposeAsync()
    {
        foreach (var file in _fileDto)
        {
            await file.Stream.DisposeAsync();
        }
    }
}
