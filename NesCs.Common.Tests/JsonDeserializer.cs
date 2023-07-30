using System.Text.Json;
using NesCs.Common.Tests.Converters;

namespace NesCs.Common.Tests;

public static class JsonDeserializer
{
    public static SampleCpu Deserialize(string jsonText) =>
        JsonSerializer.Deserialize<SampleCpu>(jsonText,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                Converters =
                {
                    new SampleRamArrayConverter(),
                    new SampleCycleArrayConverter()
                }
            }
        )!;
}