using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Application.DTOs;

public record FullNameDto
(
    string LastName,
    string FirstName,
    string Surname
);

