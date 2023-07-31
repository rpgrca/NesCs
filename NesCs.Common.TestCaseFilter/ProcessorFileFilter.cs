using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using NesCs.Common.Tests;
using NesCs.Common.Tests.Converters;

namespace NesCs.Common.TestCaseFilter;

public class ProcessorFileFilter
{
    private const string ProcessorTestFilesDirectory = "../../ProcessorTests/nes6502/v1/";
    private readonly string _filename;

    public ProcessorFileFilter(string filename) =>
        _filename = filename;

    public Dictionary<int, string> FilterUniqueCases()
    {
        var path = Path.IsPathRooted(_filename)
            ? _filename
            : Path.GetRelativePath(Directory.GetCurrentDirectory(), _filename);

        if (!File.Exists(path)) throw new ArgumentException($"Could not find file at path: {path}");

        var jsonText = File.ReadLines(_filename);
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            Converters =
            {
                new SampleRamArrayConverter(),
                new SampleCycleArrayConverter()
            }
        };

        var dictionary = new Dictionary<int, string>();
        foreach (var text in jsonText)
        {
            if (text.Length < 2)
            {
                continue;
            }

            var textWithoutComma = text.TrimEnd(',');
            var sampleCpu = JsonSerializer.Deserialize<SampleCpu>(textWithoutComma, options);
            var flagDifference = sampleCpu.Initial.P ^ sampleCpu.Final.P;
            if (! dictionary.ContainsKey(flagDifference))
            {
                dictionary.Add(flagDifference, textWithoutComma);
            }
        }

        return dictionary;
    }
}