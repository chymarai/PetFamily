using PetFamily.Application.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Application.PetsManagment.Queries.GetVolunteerById;

public record GetVolunteerByIdQuery(Guid VolunteerId) : IQueries;
