using System;

namespace AdventureTimeApi.Errors;

/// <summary>
/// Represents an exception that is thrown when there is an error loading data.
/// </summary>
public class LoadModelException : Exception
{
    public LoadModelException(Type modelName)
        : base($"Error loading {nameof(modelName)} data.")
    {
        if (modelName is null)
        {
            throw new ArgumentNullException(nameof(modelName));
        }
    }
}
