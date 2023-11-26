using System;

namespace AdventureTimeApi.Errors;

/// <summary>
/// Represents an exception that is thrown when there is an error loading data for a specific model.
/// </summary>
public class LoadModelException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="LoadModelException"/> class for a specific model.
    /// </summary>
    /// <param name="model">The type of the model for which the data loading error occurred.</param>
    public LoadModelException(Type model)
        : base($"Error loading {model?.Name ?? "unknown"} data.")
    {
        if (model is null)
        {
            throw new ArgumentNullException(nameof(model));
        }
    }
}
