using CSharpFunctionalExtensions;
using PetFamily.Domain.PetsManagment.ValueObjects.Pets;
using PetFamily.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Application.FileProvider;
public interface IFileProvider
{
    Task<Result<IReadOnlyList<FilePath>, Error>> UploadFiles(
        IEnumerable<FileData> filesData,
        CancellationToken token = default);
    Task<UnitResult<Error>> RemoveFiles(
        FileInfos fileInfo,
        CancellationToken token = default);
}