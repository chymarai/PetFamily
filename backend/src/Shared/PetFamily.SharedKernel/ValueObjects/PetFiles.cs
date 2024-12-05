namespace PetFamily.SharedKernel.ValueObjects;

public record PetFiles
{
    public PetFiles(FilePath pathToStorage)
    {
        PathToStorage = pathToStorage;
    }
    public FilePath PathToStorage { get; }

    public bool IsMain { get; }
}
