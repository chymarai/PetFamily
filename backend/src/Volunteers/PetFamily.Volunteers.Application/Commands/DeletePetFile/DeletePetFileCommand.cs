using PetFamily.Core.Abstraction;

namespace PetFamily.Volunteers.Application.Commands.DeletePetFile;
public record DeletePetFileCommand(Guid VolunteerId, Guid PetId, string FilePath) : ICommand;
