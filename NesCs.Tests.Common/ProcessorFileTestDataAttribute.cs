using System.Reflection;
using System.Text.Json;
using Xunit.Sdk;
using System.Text.Json.Serialization;

namespace NesCs.Tests.Common;

// Based on https://andrewlock.net/creating-a-custom-xunit-theory-test-dataattribute-to-load-data-from-json-files/
public class ProcessorFileTestDataAttribute : DataAttribute
{
    private readonly string _filename;

    public ProcessorFileTestDataAttribute(string filename) =>
        _filename = filename;

    public ProcessorFileTestDataAttribute(Type opcodeFile)
    {
        // Based on https://stackoverflow.com/questions/76046475/how-to-invoke-static-interface-method-via-reflection
        _filename = opcodeFile
            .GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static)
            .Where(info => info.Name == "Filename")
            .SingleOrDefault()
            ?.GetValue(null) as string ?? throw new ArgumentException($"{opcodeFile.Name} does not implement IOpcodeFile");
    }

    public override IEnumerable<object[]> GetData(MethodInfo testMethod)
    {
        if (testMethod is null) throw new ArgumentNullException(nameof(testMethod));

        var path = Path.IsPathRooted(_filename)
            ? _filename
            : Path.GetRelativePath(Directory.GetCurrentDirectory(), _filename);

        if (!File.Exists(path)) throw new ArgumentException($"Could not find file at path: {path}");

        var jsonText = File.ReadAllText(_filename);
        var datas = JsonSerializer.Deserialize<SampleCpuTest[]>(jsonText, new JsonSerializerOptions { PropertyNameCaseInsensitive = true })
            ?? throw new ArgumentNullException($"Could not deserialize {_filename} correctly");

        foreach (var data in datas)
        {
            if (string.IsNullOrWhiteSpace(data.Name)) throw new ArgumentNullException($"Empty name found in file {_filename}");
            if (data.Initial is null) throw new ArgumentNullException($"Empty initial data found in file {_filename}");
            if (data.Final is null) throw new ArgumentNullException($"Empty final data found in file {_filename}");
            if (data.Cycles is null) throw new ArgumentNullException($"Empty cycle data found in file {_filename}");

            var initialRam = new SampleRam[data.Initial.Value.RAM.Length];
            for (var index = 0; index < initialRam.Length; index++)
            {
                initialRam[index].Address = data.Initial.Value.RAM[index][0];
                initialRam[index].Value = (byte)data.Initial.Value.RAM[index][1];
            }

            var finalRam = new SampleRam[data.Final.Value.RAM.Length];
            for (var index = 0; index < finalRam.Length; index++)
            {
                finalRam[index].Address = data.Final.Value.RAM[index][0];
                finalRam[index].Value = (byte)data.Final.Value.RAM[index][1];
            }

            var cycles = new SampleCycle[data.Cycles.Length];
            for (var index = 0; index < cycles.Length; index++)
            {
                cycles[index].Address = data.Cycles[index][0].GetInt32();
                cycles[index].Value = data.Cycles[index][1].GetByte();
                cycles[index].Type = data.Cycles[index][2].GetString() ?? throw new ArgumentNullException($"Empty type found in cycle in file {_filename}");
            }

            yield return new object[]
            {
                Convert.FromHexString(data.Name.Replace(" ", string.Empty)),
                new SampleStatus
                {
                    S = data.Initial.Value.S,
                    P = data.Initial.Value.P,
                    PC = data.Initial.Value.PC,
                    X = data.Initial.Value.X,
                    Y = data.Initial.Value.Y,
                    A = data.Initial.Value.A,
                    RAM = initialRam
                },
                new SampleStatus
                {
                    S = data.Final.Value.S,
                    P = data.Final.Value.P,
                    PC = data.Final.Value.PC,
                    X = data.Final.Value.X,
                    Y = data.Final.Value.Y,
                    A = data.Final.Value.A,
                    RAM = finalRam
                },
                cycles
            };
        }
    }
}

[JsonConverter(typeof(SampleRamConverter))]
public struct SampleRam
{
    public int Address;
    public byte Value;
}

[JsonConverter(typeof(SampleCpuConverter))]
public struct SampleCpu
{
    public string Name;
    public byte[] Opcodes;
    public SampleStatus Initial;
    public SampleStatus Final;
    [JsonConverter(typeof(SampleCycleArrayConverter))]
    public SampleCycle[] Cycles;
}

[JsonConverter(typeof(SampleStatusConverter))]
public struct SampleStatus
{
    public byte S;
    public byte P;
    public int PC;
    public byte X;
    public byte Y;
    public byte A;
    [JsonConverter(typeof(SampleRamArrayConverter))]
    public SampleRam[] RAM;
}

[JsonConverter(typeof(SampleCycleConverter))]
public struct SampleCycle
{
    public int Address;
    public byte Value;
    public string Type;
}


public class ProcessorFileTest1DataAttribute : DataAttribute
{
    private readonly string _filename;

    public ProcessorFileTest1DataAttribute(Type opcodeFile)
    {
        // Based on https://stackoverflow.com/questions/76046475/how-to-invoke-static-interface-method-via-reflection
        _filename = opcodeFile
            .GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static)
            .Where(info => info.Name == "Filename")
            .SingleOrDefault()
            ?.GetValue(null) as string ?? throw new ArgumentException($"{opcodeFile.Name} does not implement IOpcodeFile");
    }

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

public class SampleStatusConverter : JsonConverter<SampleStatus>
{
    public override SampleStatus Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.StartObject) throw new JsonException();

        int pc = 0;
        byte s = 0, a = 0, x = 0, y = 0, p = 0;
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
                    p = JsonSerializer.Deserialize<byte>(ref reader, options)!;
                    break;

                case "ram":
                    ram = JsonSerializer.Deserialize<SampleRam[]>(ref reader, options);
                    break;
            }
        }

        return new SampleStatus { PC = pc, S = s, A = a, X = x, Y = y, P = p, RAM = ram };
    }

    public override void Write(Utf8JsonWriter writer, SampleStatus value, JsonSerializerOptions options) =>
        throw new NotImplementedException();
}

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

public class SampleCpuConverter : JsonConverter<SampleCpu>
{
    public override SampleCpu Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.StartObject) throw new JsonException();

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
                    break;

                case "initial":
                    initial = JsonSerializer.Deserialize<SampleStatus>(ref reader, options);
                    break;

                case "final":
                    final = JsonSerializer.Deserialize<SampleStatus>(ref reader, options);
                    break;

                case "cycles":
                    cycles = JsonSerializer.Deserialize<SampleCycle[]>(ref reader, options)!;
                    break;
            }
        }

        return new SampleCpu { Name = name, Opcodes = Convert.FromHexString(name.Replace(" ", string.Empty)), Initial = initial, Final = final, Cycles = cycles };
    }

    public override void Write(Utf8JsonWriter writer, SampleCpu value, JsonSerializerOptions options) =>
        throw new NotImplementedException();
}

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
        var type = reader.GetString();

        reader.Read();
        if (reader.TokenType != JsonTokenType.EndArray) throw new JsonException();

        return new SampleCycle { Address = address, Value = value, Type = type };
    }

    public override void Write(Utf8JsonWriter writer, SampleCycle value, JsonSerializerOptions options) =>
        throw new NotImplementedException();
}