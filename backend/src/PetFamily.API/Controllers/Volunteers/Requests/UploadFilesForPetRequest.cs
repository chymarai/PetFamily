namespace PetFamily.API.Controllers.Volunteers.Requests
{
    public record UploadFilesForPetRequest(IFormFileCollection Files);
}