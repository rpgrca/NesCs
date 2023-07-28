using System.Reflection;
using System.Text.Json;
using Xunit.Sdk;

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
            .Single()
            .GetValue(null) as string ?? throw new ArgumentException($"{opcodeFile.Name} does not implement IOpcodeFile");
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

            var cycles = new (int Address, byte Value, string Type)[data.Cycles.Length];
            for (var index = 0; index < cycles.Length; index++)
            {
                cycles[index].Address = data.Cycles[index][0].GetInt16();
                cycles[index].Value = data.Cycles[index][1].GetByte();
                cycles[index].Type = data.Cycles[index][2].GetString() ?? throw new ArgumentNullException($"Empty type found in cycle in file {_filename}");
            }

            var initialRam = new (int Address, byte Value)[data.Initial.Value.RAM.Length];
            for (var index = 0; index < initialRam.Length; index++)
            {
                initialRam[index].Address = data.Initial.Value.RAM[index][0];
                initialRam[index].Value = (byte)data.Initial.Value.RAM[index][1];
            }

            var finalRam = new (int Address, byte Value)[data.Final.Value.RAM.Length];
            for (var index = 0; index < finalRam.Length; index++)
            {
                finalRam[index].Address = data.Final.Value.RAM[index][0];
                finalRam[index].Value = (byte)data.Final.Value.RAM[index][0];
            }

            yield return new object[] {
            Convert.FromHexString(data.Name.Replace(" ", string.Empty)),
            (data.Initial.Value.S, data.Initial.Value.P, data.Initial.Value.PC, data.Initial.Value.X, data.Initial.Value.Y, data.Initial.Value.A, RAM: initialRam),
            (FS: data.Initial.Value.S, FP: data.Initial.Value.P, FPC: data.Initial.Value.PC, FX: data.Initial.Value.X, FY: data.Initial.Value.Y, FA: data.Initial.Value.A, FRAM: finalRam),
            cycles
        };
        }
    }
}