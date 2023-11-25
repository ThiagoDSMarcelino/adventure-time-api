using System;

namespace AdventureTimeApi.Errors;

/// <summary>
/// Represents an exception that is thrown when was passed an invalid specie throw the route.
/// </summary>
public class InvalidSpecieException : Exception
{
    public InvalidSpecieException(string specie)
        : base($"Specie \"{specie}\" don't exist.")
    {
    }
}