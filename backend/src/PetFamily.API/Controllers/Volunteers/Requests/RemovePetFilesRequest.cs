using PetFamily.Application.PetsManagment.Commands.UpdatePetFiles;
using PetFamily.Domain.PetsManagment.ValueObjects.Pets;

namespace PetFamily.API.Controllers.Volunteers.Requests;

public record RemovePetFilesRequest(Guid VolunteerId, Guid PetId, string FilePath)
{
    public DeletePetFileCommand ToCommand(Guid VolunteerId, Guid PetId) =>
        new(VolunteerId, PetId, FilePath);
}