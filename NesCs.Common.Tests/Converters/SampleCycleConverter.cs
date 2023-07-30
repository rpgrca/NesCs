using System.Text.Json;
using System.Text.Json.Serialization;

namespace NesCs.Tests.Common.Converters;

public class SampleCycleConverter : JsonConverter<SampleCycle>
{
    public override SampleCycle Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.StartArray) throw new JsonException();
        reader.Read();

        if (reader.TokenType != JsonTokenType.Number) throw new JsonException();
        var address = reader.GetInt32();

        reader.Read();
        if (reader.TokenType != JsonTokenType.Number) throw new JsonException();
        var value = reader.GetByte();

        reader.Read();
        if (reader.TokenType != JsonTokenType.String) throw new JsonException();
        var type = reader.GetString()!;

        reader.Read();
        if (reader.TokenType != JsonTokenType.EndArray) throw new JsonException();

        return new SampleCycle { Address = address, Value = value, Type = type };
    }

    public override void Write(Utf8JsonWriter writer, SampleCycle value, JsonSerializerOptions options) =>
        throw new NotImplementedException();
}

