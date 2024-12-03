using PetFamily.SharedKernel.ValueObjects;

namespace PetFamily.Core.FileProvider;

public record FileData(Stream Stream, FileInfos Info);

public record FileInfos(FilePath FilePath, string BucketName);
