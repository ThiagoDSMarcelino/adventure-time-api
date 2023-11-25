using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using AdventureTimeApi.Errors;

namespace AdventureTimeApi.Shared;

public static class Util
{
    public static async System.Threading.Tasks.Task<List<T>> GetFilePathAsync<T>(string path)
    {
        using FileStream stream = File.OpenRead(path);
        var data = await JsonSerializer.DeserializeAsync<List<T>>(stream) ?? throw new LoadModelException(typeof(T));

        return data;
    }

    public static bool CompareIgnoreCase(this string str1, string str2)
        => string.Equals(str1, str2, StringComparison.OrdinalIgnoreCase);
}