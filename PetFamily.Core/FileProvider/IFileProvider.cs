using CSharpFunctionalExtensions;
using PetFamily.SharedKernel;
using PetFamily.SharedKernel.ValueObjects;

namespace PetFamily.Core.FileProvider;
public interface IFileProvider
{
    Task<Result<IReadOnlyList<FilePath>, Error>> UploadFiles(
        IEnumerable<FileData> filesData,
        CancellationToken token = default);
    Task<UnitResult<Error>> RemoveFiles(
        FileInfos fileInfo,
        CancellationToken token = default);
}