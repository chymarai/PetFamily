using PetFamily.Application.PetsManagment.Commands.UpdatePetAssistanceStatus;

namespace PetFamily.API.Controllers.Volunteers.Requests;

public record UpdatePetAssistanceStatusRequest(string AssistanceStatus)
{
    public UpdatePetAssistanceStatusCommand ToCommand(Guid VolunteerId, Guid PetId) =>
        new(VolunteerId, PetId, AssistanceStatus);
}
