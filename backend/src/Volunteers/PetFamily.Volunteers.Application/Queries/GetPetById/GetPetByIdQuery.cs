using PetFamily.Core.Abstraction;

namespace PetFamily.Volunteers.Application.Queries.GetPetById;
public record GetPetByIdQuery(Guid PetId) : IQueries;
