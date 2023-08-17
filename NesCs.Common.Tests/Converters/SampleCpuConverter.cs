using System.Text.Json;
using System.Text.Json.Serialization;

namespace NesCs.Common.Tests.Converters;

public class SampleCpuConverter : JsonConverter<SampleCpu>
{
    public override SampleCpu Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.StartObject) throw new JsonException();

        byte opcode = 0;
        var name = string.Empty;
        var initial = new SampleStatus();
        var final = new SampleStatus();
        var cycles = Array.Empty<SampleCycle>();

        while (reader.Read() && reader.TokenType != JsonTokenType.EndObject)
        {
            if (reader.TokenType != JsonTokenType.PropertyName) throw new JsonException();

            var propertyName = reader.GetString() ?? throw new Exception("Empty property name");
            switch (propertyName)
            {
                case "name":
                    name = JsonSerializer.Deserialize<string>(ref reader, options)!;
                    opcode = Convert.FromHexString(name[0..2])[0];
                    break;

                case "initial":
                    initial = JsonSerializer.Deserialize<SampleStatus>(ref reader, options)!;
                    break;

                case "final":
                    final = JsonSerializer.Deserialize<SampleStatus>(ref reader, options)!;
                    break;

                case "cycles":
                    cycles = JsonSerializer.Deserialize<SampleCycle[]>(ref reader, options)!;
                    break;
            }
        }

        return new SampleCpu { Opcode = opcode, Name = name, Initial = initial, Final = final, Cycles = cycles };
    }

    public override void Write(Utf8JsonWriter writer, SampleCpu value, JsonSerializerOptions options) =>
        throw new NotImplementedException();
}

