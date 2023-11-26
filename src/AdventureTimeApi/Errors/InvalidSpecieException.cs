using System;

namespace AdventureTimeApi.Errors;

/// <summary>
/// Represents an exception that is thrown when an invalid species is passed through the route.
/// </summary>
public class InvalidSpecieException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="InvalidSpecieException"/> class with a specific species.
    /// </summary>
    /// <param name="specie">The invalid species that caused the exception.</param>
    public InvalidSpecieException(string specie)
        : base($"Species \"{specie}\" does not exist.")
    {
    }
}
