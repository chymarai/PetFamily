using PetFamily.Domain.Modules.Volunteers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Application.DTOs;
public record RequisiteDetailsDto(IEnumerable<RequisiteDto> Requisite);
