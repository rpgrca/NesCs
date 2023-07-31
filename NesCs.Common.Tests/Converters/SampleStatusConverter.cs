using System.Text.Json;
using System.Text.Json.Serialization;
using NesCs.Common.Tests;
using static NesCs.Logic.Cpu.Cpu6502;

namespace NesCs.Common.Tests.Converters;

public class SampleStatusConverter : JsonConverter<SampleStatus>
{
    public override SampleStatus Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.StartObject) throw new JsonException();

        int pc = 0;
        byte s = 0, a = 0, x = 0, y = 0;
        ProcessorStatus p = ProcessorStatus.None;
        SampleRam[] ram = Array.Empty<SampleRam>();

        while (reader.Read())
        {
            if (reader.TokenType == JsonTokenType.EndObject)
            {
                break;
            }

            if (reader.TokenType != JsonTokenType.PropertyName) throw new JsonException();

            var propertyName = reader.GetString() ?? throw new Exception("Empty property name");
            switch (propertyName)
            {
                case "pc":
                    pc = JsonSerializer.Deserialize<int>(ref reader, options)!;
                    break;

                case "s":
                    s = JsonSerializer.Deserialize<byte>(ref reader, options)!;
                    break;

                case "a":
                    a = JsonSerializer.Deserialize<byte>(ref reader, options)!;
                    break;

                case "x":
                    x = JsonSerializer.Deserialize<byte>(ref reader, options)!;
                    break;

                case "y":
                    y = JsonSerializer.Deserialize<byte>(ref reader, options)!;
                    break;

                case "p":
                    p = JsonSerializer.Deserialize<ProcessorStatus>(ref reader, options)!;
                    break;

                case "ram":
                    ram = JsonSerializer.Deserialize<SampleRam[]>(ref reader, options)!;
                    break;
            }
        }

        return new SampleStatus { PC = pc, S = s, A = a, X = x, Y = y, P = p, RAM = ram };
    }

    public override void Write(Utf8JsonWriter writer, SampleStatus value, JsonSerializerOptions options) =>
        throw new NotImplementedException();
}