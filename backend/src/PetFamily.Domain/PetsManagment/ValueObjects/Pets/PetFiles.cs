namespace PetFamily.Domain.PetsManagment.ValueObjects.Pets;

public record PetFiles
{
    public PetFiles(FilePath pathToStorage)
    {
        PathToStorage = pathToStorage;
    }
    public FilePath PathToStorage { get; }
}
