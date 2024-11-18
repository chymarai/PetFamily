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
    IQueryable<VolunteerDto> Volunteers { get;}
    IQueryable<PetDto> Pets { get;}
}
