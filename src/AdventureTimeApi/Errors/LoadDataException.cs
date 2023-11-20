using System;

namespace AdventureTimeApi.Errors;

/// <summary>
/// Represents an exception that is thrown when there is an error loading data.
/// </summary>
public class LoadModelException : Exception
{
    public LoadModelException(Model model)
        : base($"Error loading {model.GetType().Name} data.")
    {
    }
}
