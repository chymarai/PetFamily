using PetFamily.Core.Abstraction;

namespace PetFamily.Volunteers.Application.Commands.UpdatePetAssistanceStatus;
public record UpdatePetAssistanceStatusCommand(
    Guid VolunteerId,
    Guid PetId,
    string AssistanceStatus) : ICommand;
