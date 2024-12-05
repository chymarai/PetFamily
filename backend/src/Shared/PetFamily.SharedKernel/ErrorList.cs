﻿using System.Collections;

namespace PetFamily.SharedKernel;

public class ErrorList : IEnumerable<Error>
{
    private List<Error> _errors;

    public ErrorList(IEnumerable<Error> errors)
    {
        _errors = errors.ToList();
    }

    public IEnumerator<Error> GetEnumerator()
    {
        return _errors.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public static implicit operator ErrorList(List<Error> error)
    {
        return new ErrorList(error);
    }

    public static implicit operator ErrorList(Error error)
    {
        return new ErrorList([error]);
    }
}
