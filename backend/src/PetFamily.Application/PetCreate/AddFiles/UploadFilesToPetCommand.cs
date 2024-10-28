using PetFamily.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Application.Pet.AddFiles;
public record UploadFilesToPetCommand(Guid VolunteerId, Guid PetId, IEnumerable<UploadFileDto> Files);
