using System.Text.Json;
using System.Text.Json.Serialization;

namespace NesCs.Common.Tests.Converters;

public class SampleCycleArrayConverter : JsonConverter<SampleCycle[]>
{
    public override SampleCycle[] Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var result = new List<SampleCycle>();

        if (reader.TokenType != JsonTokenType.StartArray) throw new JsonException();

        do
        {
            while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
            {
                var cycle = JsonSerializer.Deserialize<SampleCycle>(ref reader, options);
                result.Add(cycle);
            }
        }
        while (reader.TokenType != JsonTokenType.EndArray);
        return result.ToArray();
    }

    public override void Write(Utf8JsonWriter writer, SampleCycle[] value, JsonSerializerOptions options) =>
        throw new NotImplementedException();
}