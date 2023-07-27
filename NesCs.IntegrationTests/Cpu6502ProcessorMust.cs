using System.Text.Json;

namespace NesCs.IntegrationTests;

public class Cpu6502ProcessorMust
{
	[Theory]
    [MemberData(nameof(OpcodeB5JsonFeeder))]
    public void Execute10000DifferentB5SampleTestsCorrectly(NesCs.UnitTests.SampleCpuTest data)
    {
        var trace = new List<(int, byte, string)>();
        var sut = new Cpu6502.Builder()
            .RunningProgram(Convert.FromHexString(data.Name.Replace(" ", string.Empty)).Take(2).ToArray())
            .WithRamSizeOf(0xffff)
            .WithStackPointerAt(data.Initial.Value.S)
            .WithProcessorStatusAs(data.Initial.Value.P)
            .WithXAs(data.Initial.Value.X)
            .WithYAs(data.Initial.Value.Y)
            .WithAccumulatorAs(data.Initial.Value.A)
            .WithProgramCounterAs(data.Initial.Value.PC)
            .RamPatchedAs(data.Initial.Value.RAM)
            .TracingWith(trace)
            .Build();

        sut.Run();

        Assert.Equal(data.Final.Value.S, sut.S);
        Assert.Equal(data.Final.Value.X, sut.X);
        Assert.Equal(data.Final.Value.Y, sut.Y);
        Assert.Equal(data.Final.Value.A, sut.A);
        Assert.Equal(data.Final.Value.P, (byte)sut.P);
        Assert.Equal(data.Final.Value.PC, sut.PC);
        foreach (var memory in data.Final.Value.RAM)
        {
            Assert.Equal(memory[1], sut.PeekMemory(memory[0]));
        }
  
        for (var index = 0; index < data.Cycles.Length; index++)
        {
            Assert.Equal(data.Cycles[index][0].GetInt32(), trace[index].Item1);
            Assert.Equal(data.Cycles[index][1].GetByte(), trace[index].Item2);
            Assert.Equal(data.Cycles[index][2].GetString(), trace[index].Item3);
        }
    }

    public static IEnumerable<object[]> OpcodeB5JsonFeeder()
    {
        var jsonText = System.IO.File.ReadAllText("../../../../../ProcessorTests/nes6502/v1/b5.json");
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
        var datas = JsonSerializer.Deserialize<NesCs.UnitTests.SampleCpuTest[]>(jsonText, options);

        foreach (var data in datas)
        {
            yield return new object[] { data };
        }
    }
}