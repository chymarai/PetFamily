using CSharpFunctionalExtensions;
using PetFamily.SharedKernel;

namespace PetFamily.Volunteers.Domain.PetsValueObjects;

public record Position
{
    public static Position First = new(1);
    private Position(int value)
    {
        Value = value;
    }

    public int Value { get; }

    public static Result<Position, Error> Create(int number)
    {
        if (number <= 0)
            return Errors.General.ValueIsInvalid("serial number");

        return new Position(number);
    }

    public Result<Position, Error> Forward() => Create(Value + 1);

    public Result<Position, Error> Back() => Create(Value - 1);
}
