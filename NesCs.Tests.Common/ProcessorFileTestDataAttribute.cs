using System.Reflection;
using System.Text.Json;
using Xunit.Sdk;
using NesCs.Tests.Common.Converters;

namespace NesCs.Tests.Common;

// Based on https://andrewlock.net/creating-a-custom-xunit-theory-test-dataattribute-to-load-data-from-json-files/
public class ProcessorFileTestDataAttribute : DataAttribute
{
    private const string ProcessorTestFilesDirectory = "../../../../../ProcessorTests/nes6502/v1/";
    private readonly string _filename;

    public ProcessorFileTestDataAttribute(string opcode) =>
        _filename = $"{ProcessorTestFilesDirectory}/{opcode}.json";

    public override IEnumerable<object[]> GetData(MethodInfo testMethod)
    {
        if (testMethod is null) throw new ArgumentNullException(nameof(testMethod));

        var path = Path.IsPathRooted(_filename)
            ? _filename
            : Path.GetRelativePath(Directory.GetCurrentDirectory(), _filename);

        if (!File.Exists(path)) throw new ArgumentException($"Could not find file at path: {path}");

        var jsonText = File.ReadAllText(_filename);
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            Converters =
            {
                new SampleRamArrayConverter(),
                new SampleCycleArrayConverter()
            }
        };

        foreach (var sampleCpu in JsonSerializer.Deserialize<SampleCpu[]>(jsonText, options)
            ?? throw new ArgumentNullException($"Could not deserialize {_filename} correctly"))
        {
            object[] result = { sampleCpu };
            yield return result;
        }
    }
}