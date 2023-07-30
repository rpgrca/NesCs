using System.Text.Json;
using System.Text.Json.Serialization;

namespace NesCs.Common.Tests.Converters;

public class SampleRamArrayConverter : JsonConverter<SampleRam[]>
{
    public override SampleRam[] Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var result = new List<SampleRam>();

        if (reader.TokenType != JsonTokenType.StartArray) throw new JsonException();

        do
        {
            while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
            {
                var ram = JsonSerializer.Deserialize<SampleRam>(ref reader, options);
                result.Add(ram);
            }
        }
        while (reader.TokenType != JsonTokenType.EndArray);
        return result.ToArray();
    }

    public override void Write(Utf8JsonWriter writer, SampleRam[] value, JsonSerializerOptions options) =>
        throw new NotImplementedException();
}

