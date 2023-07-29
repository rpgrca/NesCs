using System.Text.Json;
using System.Text.Json.Serialization;

namespace NesCs.Tests.Common.Converters;

public class SampleRamConverter : JsonConverter<SampleRam>
{
    public override SampleRam Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.StartArray) throw new JsonException();
        reader.Read();

        if (reader.TokenType != JsonTokenType.Number) throw new JsonException();
        var address = reader.GetInt32();

        reader.Read();
        if (reader.TokenType != JsonTokenType.Number) throw new JsonException();
        var value = reader.GetByte();

        reader.Read();
        if (reader.TokenType != JsonTokenType.EndArray) throw new JsonException();

        return new SampleRam { Address = address, Value = value };
    }

    public override void Write(Utf8JsonWriter writer, SampleRam value, JsonSerializerOptions options) =>
        throw new NotImplementedException();
}

