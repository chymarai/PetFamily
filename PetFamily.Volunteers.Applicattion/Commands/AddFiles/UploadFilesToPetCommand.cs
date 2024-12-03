using PetFamily.Core.Abstraction;
using PetFamily.Core.DTOs;

namespace PetFamily.Volunteers.Application.Commands.AddFiles;
public record UploadFilesToPetCommand(
    Guid VolunteerId,
    Guid PetId,
    IEnumerable<UploadFileDto> Files) : ICommand;
