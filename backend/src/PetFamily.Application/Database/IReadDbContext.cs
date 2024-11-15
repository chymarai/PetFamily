using Microsoft.EntityFrameworkCore;
using PetFamily.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Application.Database;
public interface IReadDbContext
{
    DbSet<VolunteerDto> Volunteers { get;}
    DbSet<PetDto> Pets { get;}
}
