using PetFamily.Application.Pet.AddFiles;
using PetFamily.Application.Pet.Create;

namespace PetFamily.API.Controllers.Volunteers.Requests
{
    public record UploadFilesForPetRequest(IFormFileCollection Files);
}