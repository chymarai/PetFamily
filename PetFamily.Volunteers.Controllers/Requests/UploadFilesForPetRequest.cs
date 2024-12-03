using Microsoft.AspNetCore.Http;

namespace PetFamily.Volunteers.Presentation.Requests
{
    public record UploadFilesForPetRequest(IFormFileCollection Files);
}