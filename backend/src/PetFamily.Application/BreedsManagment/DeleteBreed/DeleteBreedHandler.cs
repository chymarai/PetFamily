using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PetFamily.Application.Abstraction;
using PetFamily.Application.Database;
using PetFamily.Application.Extensions;
using PetFamily.Application.Specieses;
using PetFamily.Domain.PetsManagment.ValueObjects.Pets;
using PetFamily.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Application.BreedsManagment.DeleteBreed;
public class DeleteBreedHandler : ICommandHandler<Guid, DeleteBreedCommand>
{
    private readonly ISpeciesesRepository _speciesesRepository;
    private readonly IReadDbContext _readDbContext;
    private readonly IValidator<DeleteBreedCommand> _validator;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<DeleteBreedCommand> _logger;

    public DeleteBreedHandler(
        ISpeciesesRepository speciesesRepository,
        IReadDbContext readDbContext,
        IValidator<DeleteBreedCommand> validator,
        IUnitOfWork unitOfWork,
        ILogger<DeleteBreedCommand> logger
        )
    {
        _speciesesRepository = speciesesRepository;
        _readDbContext = readDbContext;
        _validator = validator;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Result<Guid, ErrorList>> Handle(DeleteBreedCommand command, CancellationToken token)
    {
        var validationResult = await _validator.ValidateAsync(command, token);
        if (validationResult.IsValid == false)
            return validationResult.ToErrorList();

        var breedId = BreedId.Create(command.BreedId).Value;

        var petResult = await _readDbContext.Pets.FirstOrDefaultAsync(p => p.BreedId == breedId, token);
        if (petResult != null)
            return Errors.Breed.Exist(breedId).ToErrorList();

        var speciesResult = await _speciesesRepository.GetById(command.SpeciesId);
        if (speciesResult.IsFailure)
            return speciesResult.Error.ToErrorList();

        var breed = speciesResult.Value.Breeds.FirstOrDefault(b => b.Id.Value == breedId);
        if (breed is null)
            return Errors.General.NotFound().ToErrorList();

        speciesResult.Value.DeleteBreed(breed);

        await _unitOfWork.SaveChanges(token);

        _logger.LogInformation("Updates deleted with id {breedId}", breedId);

        return breedId;
    }
}
