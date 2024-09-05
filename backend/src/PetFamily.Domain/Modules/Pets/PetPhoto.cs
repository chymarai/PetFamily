using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetFamily.Domain.Shared;

namespace PetFamily.Domain.Modules.Pets
{
    public record PetPhoto
    {
        private PetPhoto(string storage, bool isMain)
        {
            Storage = storage;
            IsMain = isMain;
        }
        public string Storage { get; }
        public bool IsMain { get; }

        public static Result<PetPhoto> Create(string storage, bool isMain)
        {
            if (string.IsNullOrWhiteSpace(storage))
                return "FilePath can not be empty or null";

            return new PetPhoto(storage, isMain);
        }
    }
}
