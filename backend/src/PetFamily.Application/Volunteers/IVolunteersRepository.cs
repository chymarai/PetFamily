﻿using CSharpFunctionalExtensions;
using PetFamily.Domain.Modules.Volunteers;
using PetFamily.Domain.Shared;

namespace PetFamily.Infrastructure.Repositories;

public interface IVolunteersRepository
{
    Task<Guid> Add(Volunteer volunteer, CancellationToken cancellationToken = default);
    Task<Guid> Update(Volunteer volunteer, CancellationToken cancellationToken = default);
    Task<Result<Volunteer, Error>> GetById(VolunteerId volunteerId, CancellationToken cancellationToken = default);
    Task<Result<Volunteer, Error>> GetByEmail(Email email, CancellationToken cancellationToken = default);
} 