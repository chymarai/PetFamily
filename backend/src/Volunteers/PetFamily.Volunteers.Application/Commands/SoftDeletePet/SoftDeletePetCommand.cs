using PetFamily.Core.Abstraction;

namespace PetFamily.Volunteers.Application.Commands.SoftDeletePet;
public record SoftDeletePetCommand(Guid VolunteerId, Guid PetId) : ICommand;
