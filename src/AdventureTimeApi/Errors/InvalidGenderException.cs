using System;

namespace AdventureTimeApi.Errors;

/// <summary>
/// Represents an exception that is thrown when an invalid gender is passed through the route.
/// </summary>
public class InvalidGenderException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="InvalidGenderException"/> class with a specific gender.
    /// </summary>
    /// <param name="gender">The invalid gender that caused the exception.</param>
    public InvalidGenderException(string gender)
        : base($"Gender \"{gender}\" does not exist.")
    {
    }
}
