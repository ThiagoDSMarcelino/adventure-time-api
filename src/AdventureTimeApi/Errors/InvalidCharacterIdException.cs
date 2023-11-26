using System;

namespace AdventureTimeApi.Errors;

/// <summary>
/// Represents an exception that is thrown when an invalid character ID is encountered.
/// </summary>
public class InvalidCharacterIdException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="InvalidCharacterIdException"/> class with a specific character ID.
    /// </summary>
    /// <param name="id">The invalid character ID that caused the exception.</param>
    public InvalidCharacterIdException(uint id)
        : base($"ID \"{id}\" does not exist.")
    {
    }
}
