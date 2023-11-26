using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using AdventureTimeApi.Errors;

namespace AdventureTimeApi.Shared;

/// <summary>
/// Utility class containing common methods used in the Adventure Time API.
/// </summary>
public static class Util
{
    /// <summary>
    /// Reads data from a file asynchronously and deserializes it into a list of the specified type.
    /// </summary>
    /// <typeparam name="T">The type of the data to be deserialized.</typeparam>
    /// <param name="path">The file path from which to read the data.</param>
    /// <returns>A task representing the asynchronous operation. The task result is a list of the specified type.</returns>
    public static async System.Threading.Tasks.Task<List<T>> GetFilePathAsync<T>(string path)
    {
        using FileStream stream = File.OpenRead(path);
        var data = await JsonSerializer.DeserializeAsync<List<T>>(stream) ?? throw new LoadModelException(typeof(T));

        return data;
    }

    /// <summary>
    /// Compares two strings for equality, ignoring case.
    /// </summary>
    /// <param name="left">The first string to compare.</param>
    /// <param name="right">The second string to compare.</param>
    /// <returns>True if the strings are equal, ignoring case; otherwise, false.</returns>
    public static bool CompareIgnoreCase(this string left, string right)
        => string.Equals(left, right, StringComparison.OrdinalIgnoreCase);
}
