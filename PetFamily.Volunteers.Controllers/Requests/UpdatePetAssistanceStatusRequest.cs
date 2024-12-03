using PetFamily.Volunteers.Application.Commands.UpdatePetAssistanceStatus;

namespace PetFamily.Volunteers.Presentation.Requests;

public record UpdatePetAssistanceStatusRequest(string AssistanceStatus)
{
    public UpdatePetAssistanceStatusCommand ToCommand(Guid VolunteerId, Guid PetId) =>
        new(VolunteerId, PetId, AssistanceStatus);
}
