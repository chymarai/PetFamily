namespace PetFamily.Framework.Authorization;
public class Permissions
{
    public class Accounts
    {

    }
    public class Volunteers
    {
        public const string CreateVolunteer = "volunteer.create";
        public const string UpdateVolunteer = "volunteer.update";
        public const string DeleteVolunteer = "volunteer.delete";
        public const string GetVolunteer = "volunteer.get";
    }

    public class Pets
    {
        public const string CreatePet = "pet.create";
        public const string UpdatePet = "pet.update";
        public const string SoftDeletePet = "pet.soft.delete";
        public const string HardDeletePet = "pet.hard.delete";
        public const string GetPet = "pet.get";
        public const string UpdateFile = "pet.file.update";
        public const string DeleteFile = "pet.file.delete";
    }

    public class Specieses
    {
        public const string CreateSpecies = "species.create";
        public const string DeleteSpecies = "species.delete";
        public const string GetSpecies = "species.get";
    }

    public class Breeds
    {
        public const string CreateBreed = "breed.create";
        public const string DeleteBreed = "breed.delete";
        public const string GetBreed = "breed.get";
    }
}
