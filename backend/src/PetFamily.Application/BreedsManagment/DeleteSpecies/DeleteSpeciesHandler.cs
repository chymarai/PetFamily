
using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PetFamily.Application.Abstraction;
using PetFamily.Application.Database;
using PetFamily.Application.DTOs;
using PetFamily.Application.Extensions;
using PetFamily.Application.PetsManagment.Queries;
using PetFamily.Application.Specieses;
using PetFamily.Domain.Shared;
using PetFamily.Domain.SpeciesManagment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PetFamily.Application.BreedsManagment.DeleteSpecies;
public class DeleteSpeciesHandler : ICommandHandler<Guid, DeleteSpeciesCommand>
{
    private readonly ISpeciesesRepository _speciesesRepository;
    private readonly IReadDbContext _readDbContext;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<DeleteSpeciesHandler> _logger;
    private readonly IValidator<DeleteSpeciesCommand> _validator;

    public DeleteSpeciesHandler(
        ISpeciesesRepository speciesesRepository,
        IReadDbContext readDbContext,
        IUnitOfWork unitOfWork,
        ILogger<DeleteSpeciesHandler> logger,
        IValidator<DeleteSpeciesCommand> validator)
    {
        _speciesesRepository = speciesesRepository;
        _readDbContext = readDbContext;
        _unitOfWork = unitOfWork;
        _logger = logger;
        _validator = validator;
    }
    public async Task<Result<Guid, ErrorList>> Handle(DeleteSpeciesCommand command, CancellationToken token)
    {
        var validationResult = await _validator.ValidateAsync(command, token);
        if (validationResult.IsValid == false)
            return validationResult.ToErrorList();

        var petResult = await _readDbContext.Pets.FirstOrDefaultAsync(p => p.SpeciesId == command.SpeciesId, token);
        if (petResult != null)
            return Errors.Species.Exist(command.SpeciesId).ToErrorList();

        var speciesResult = await _speciesesRepository.GetById(command.SpeciesId, token);
        if (speciesResult.IsFailure) 
            return speciesResult.Error.ToErrorList();

        var result = _speciesesRepository.Delete(speciesResult.Value, token);

        await _unitOfWork.SaveChanges(token);

        _logger.LogInformation("Updates deleted with id {speciesId}", speciesResult);

        return result;
    }
}
