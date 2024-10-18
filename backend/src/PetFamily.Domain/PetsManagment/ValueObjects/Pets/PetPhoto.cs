using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;

namespace PetFamily.Domain.PetsManagment.ValueObjects.Pets;

public record PetPhoto
{
    private PetPhoto(string storage, bool isMain)
    {
        Storage = storage;
        IsMain = isMain;
    }
    public string Storage { get; }
    public bool IsMain { get; }

    public static Result<PetPhoto, Error> Create(string storage, bool isMain)
    {
        if (string.IsNullOrWhiteSpace(storage) || storage.Length > Constants.MAX_LOW_TEXT_LENGTH)
            return Errors.General.ValueIsInvalid("Storage");

        return new PetPhoto(storage, isMain);
    }
}
