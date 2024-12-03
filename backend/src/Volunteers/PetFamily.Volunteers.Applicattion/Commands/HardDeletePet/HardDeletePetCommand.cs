using PetFamily.Core.Abstraction;

namespace PetFamily.Volunteers.Application.Commands.HardDeletePet;
public record HardDeletePetCommand(Guid VolunteerId, Guid PetId) : ICommand;
