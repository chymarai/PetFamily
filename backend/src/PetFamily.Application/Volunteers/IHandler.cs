using CSharpFunctionalExtensions;
using PetFamily.Application.Volunteers.WriteHandler.Create;
using PetFamily.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Application.Volunteers;
interface IHandler<TCommand>
{

    Task<Result<Guid, ErrorList>> Handle(
       TCommand command,
       CancellationToken token = default);
}
