using FluentAssertions;
using PetFamily.SharedKernel.Ids;
using PetFamily.SharedKernel.ValueObjects;
using PetFamily.Volunteers.Domain;
using PetFamily.Volunteers.Domain.PetsValueObjects;
using PetFamily.Volunteers.Domain.VolunteersValueObjects;

namespace PetFamily.UnitTests;

public class VolunteersTests
{
    [Fact]
    public void Add_Pet_First_Return_Success()
    {
        // arrange - подготовка к тесту

        var fullName = FullName.Create("Test", "Test", "Test").Value;

        var email = Email.Create("Test").Value;
        var phoneNumber = PhoneNumber.Create("Test").Value;
        var description = Description.Create("Test").Value;
        var experience = Experience.Create("Test").Value;

        var socialNetwork = SocialNetwork.Create("Test", "Test").Value;
        List<SocialNetwork> socialNetworks = [socialNetwork];
        var socialNetworkDetails = SocialNetworkDetails.Create(socialNetworks);

        var requisite = Requisite.Create("Test", "Test").Value;
        List<Requisite> requisites = [requisite];
        var requisiteDetails = RequisiteDetails.Create(requisites);

        var volunteerId = VolunteerId.NewVolunteerId();

        var volunteer = new Volunteer(
            volunteerId,
            fullName,
            email,
            phoneNumber,
            description,
            experience,
            socialNetworkDetails,
            requisiteDetails);

        var name = Name.Create("Test").Value;
        var descriptionPet = Description.Create("Test").Value;

        var speciesId = SpeciesId.NewSpeciesId().Value;
        var breedId = BreedId.NewBreedId().Value;
        var speciesBreed = SpeciesBreed.Create(speciesId, breedId).Value;

        var color = Color.Create("Test").Value;
        var healthInformation = HealthInformation.Create("Test").Value;

        var address = Address.Create(
            "Test",
            "Test",
            "Test",
            "Test").Value;

        var weight = Weight.Create(1).Value;
        var height = Height.Create(1).Value;
        var phoneNumberPet = PhoneNumber.Create("Test").Value;

        var assistanceStatus = Enum.Parse<AssistanceStatus>("AtHome", true); ;

        var birthdate = BirthDate.Create(DateTime.Now).Value;

        var requisitePet = Requisite.Create("Test", "Test").Value;
        List<Requisite> requisitesPet = [requisitePet];
        var requisiteDetailsPet = RequisiteDetails.Create(requisitesPet);

        var petId = PetId.NewPetId();

        var pet = new Pet(
            petId,
            name,
            description,
            speciesBreed,
            color,
            healthInformation,
            address,
            weight,
            height,
            phoneNumber,
            true,
            true,
            assistanceStatus,
            birthdate,
            DateTime.Now,
            requisiteDetailsPet,
            null);

        // act - вызов тестируемого метода

        var result = volunteer.AddPet(pet);

        // assert - проверка результата

        var addedPetResult = volunteer.GetPetById(petId);

        result.IsSuccess.Should().BeTrue();
        addedPetResult.IsSuccess.Should().BeTrue();
        addedPetResult.Value.Id.Should().Be(pet.Id);
        addedPetResult.Value.Position.Should().Be(pet.Position);
    }

    [Fact]
    public void Shift_Pet_Position_From_Less_To_More()
    {
        const int petCount = 5;

        var volunteer = CreateVolunteerWithPets(petCount);

        var firstPet = volunteer.Pets[0];
        var secondPet = volunteer.Pets[1];
        var thirdPet = volunteer.Pets[2];
        var fourthPet = volunteer.Pets[3];
        var fifthPet = volunteer.Pets[4];

        var newPosition = Position.Create(50).Value;

        var result = volunteer.ShiftPetPosition(secondPet, newPosition);

        result.IsSuccess.Should().BeTrue();
        firstPet.Position.Value.Should().Be(1);
        secondPet.Position.Value.Should().Be(5);
        thirdPet.Position.Value.Should().Be(2);
        fourthPet.Position.Value.Should().Be(3);
        fifthPet.Position.Value.Should().Be(4);
    }

    [Fact]
    public void Shift_Pet_Position_From_More_To_Less()
    {
        const int petCount = 5;

        var volunteer = CreateVolunteerWithPets(petCount);

        var firstPet = volunteer.Pets[0];
        var secondPet = volunteer.Pets[1];
        var thirdPet = volunteer.Pets[2];
        var fourthPet = volunteer.Pets[3];
        var fifthPet = volunteer.Pets[4];
        var sixthPet = volunteer.Pets[5];

        var newPosition = Position.Create(2).Value;

        var result = volunteer.ShiftPetPosition(fifthPet, newPosition);

        result.IsSuccess.Should().BeTrue();
        firstPet.Position.Value.Should().Be(1);
        secondPet.Position.Value.Should().Be(3);
        thirdPet.Position.Value.Should().Be(4);
        fourthPet.Position.Value.Should().Be(5);
        fifthPet.Position.Value.Should().Be(2);
        sixthPet.Position.Value.Should().Be(6);
    }

    private Volunteer CreateVolunteerWithPets(int petCount)
    {
        var fullName = FullName.Create("Test", "Test", "Test").Value;

        var email = Email.Create("Test").Value;
        var phoneNumber = PhoneNumber.Create("Test").Value;
        var description = Description.Create("Test").Value;
        var experience = Experience.Create("Test").Value;

        var socialNetwork = SocialNetwork.Create("Test", "Test").Value;
        List<SocialNetwork> socialNetworks = [socialNetwork];
        var socialNetworkDetails = SocialNetworkDetails.Create(socialNetworks);

        var requisite = Requisite.Create("Test", "Test").Value;
        List<Requisite> requisites = [requisite];
        var requisiteDetails = RequisiteDetails.Create(requisites);

        var volunteerId = VolunteerId.NewVolunteerId();

        var volunteer = new Volunteer(
            volunteerId,
            fullName,
            email,
            phoneNumber,
            description,
            experience,
            socialNetworkDetails,
            requisiteDetails);

        var name = Name.Create("Test").Value;
        var descriptionPet = Description.Create("Test").Value;

        var speciesId = SpeciesId.NewSpeciesId().Value;
        var breedId = BreedId.NewBreedId().Value;
        var speciesBreed = SpeciesBreed.Create(speciesId, breedId).Value;

        var color = Color.Create("Test").Value;
        var healthInformation = HealthInformation.Create("Test").Value;

        var address = Address.Create(
            "Test",
            "Test",
            "Test",
            "Test").Value;

        var weight = Weight.Create(1).Value;
        var height = Height.Create(1).Value;
        var phoneNumberPet = PhoneNumber.Create("Test").Value;

        var assistanceStatus = Enum.Parse<AssistanceStatus>("AtHome", true); ;

        var birthdate = BirthDate.Create(DateTime.Now).Value;

        var requisitePet = Requisite.Create("Test", "Test").Value;
        List<Requisite> requisitesPet = [requisitePet];
        var requisiteDetailsPet = RequisiteDetails.Create(requisitesPet);

        for (int i = 0; i < petCount; i++)
        {
            var pet = new Pet(
            PetId.NewPetId(),
            name,
            descriptionPet,
            speciesBreed,
            color,
            healthInformation,
            address,
            weight,
            height,
            phoneNumberPet,
            true,
            true,
            assistanceStatus,
            birthdate,
            DateTime.Now,
            requisiteDetailsPet,
            null);

            volunteer.AddPet(pet);
        }

        return volunteer;
    }
}