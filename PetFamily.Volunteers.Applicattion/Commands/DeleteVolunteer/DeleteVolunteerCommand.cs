using PetFamily.Core.Abstraction;

namespace PetFamily.Volunteers.Application.Commands.DeleteVolunteer;
public record DeleteVolunteerCommand(Guid VolunteerId) : ICommand;