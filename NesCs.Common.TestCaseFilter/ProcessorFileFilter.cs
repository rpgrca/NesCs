using System.Text.Json;
using NesCs.Common.Tests;
using NesCs.Common.Tests.Converters;
using NesCs.Logic.Cpu;

namespace NesCs.Common.TestCaseFilter;

public class ProcessorFileFilter
{
    private readonly string _filename;

    public ProcessorFileFilter(string filename) =>
        _filename = filename;

    public Dictionary<ProcessorStatus, string> FilterUniqueCases()
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

        var dictionary = new Dictionary<ProcessorStatus, string>();
        foreach (var text in jsonText)
        {
            if (text.Length < 2)
            {
                continue;
            }

            var textWithoutComma = text.TrimStart('[').TrimEnd(',').TrimEnd(']');
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