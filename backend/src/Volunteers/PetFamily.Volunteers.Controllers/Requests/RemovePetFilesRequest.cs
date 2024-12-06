using PetFamily.Volunteers.Application.Commands.DeletePetFile;

namespace PetFamily.Volunteers.Presentation.Requests;

public record RemovePetFilesRequest(Guid VolunteerId, Guid PetId, string FilePath)
{
    public DeletePetFileCommand ToCommand(Guid VolunteerId, Guid PetId) =>
        new(VolunteerId, PetId, FilePath);
}