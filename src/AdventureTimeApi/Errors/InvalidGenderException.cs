using System;

namespace AdventureTimeApi.Errors;

/// <summary>
/// Represents an exception that is thrown when was passed an invalid gender throw the route.
/// </summary>
public class InvalidGenderException : Exception
{
    public InvalidGenderException(string gender)
        : base($"Gender \"{gender}\" don't exist.")
    {
    }
}