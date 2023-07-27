using System.Text.Json;
using NesCs.Tests.Common;

namespace NesCs.UnitTests;

public class Cpu6502ProcessorMust
{
	[Theory]
    [MemberData(nameof(OpcodeB5JsonFeeder))]
    public void ExecuteOpcodeB5Correctly(string jsonText)
    {
        var data = JsonSerializer.Deserialize<SampleCpuTest>(jsonText, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
		var trace = new List<(int, byte, string)>();
		var sut = Utilities.CreateSubjectUnderTestFromSample(data, trace);
		sut.Run();

		Utilities.Equal(data.Final, sut);
		Utilities.Equal(data.Cycles, trace);
    }

	public static IEnumerable<object[]> OpcodeB5JsonFeeder()
	{
		yield return new object[] { """
{
	"name": "b5 6d 7e",
	"initial": {
		"pc": 27808,
		"s": 113,
		"a": 156,
		"x": 116,
		"y": 192,
		"p": 233,
		"ram": [
			[ 27808, 181 ],
			[ 27809, 109 ],
			[ 27810, 126 ],
			[ 109, 139 ],
			[ 225, 49 ]
		]
	},
	"final": {
		"pc": 27810,
		"s": 113,
		"a": 49,
		"x": 116,
		"y": 192,
		"p": 105,
		"ram": [
			[ 109, 139 ],
			[ 225, 49 ],
			[ 27808, 181 ],
			[ 27809, 109 ],
			[ 27810, 126 ]
		]
	},
	"cycles": [
		[ 27808, 181, "read" ],
		[ 27809, 109, "read" ],
		[ 109, 139, "read" ],
		[ 225, 49, "read" ]
	]
}
""" };
		yield return new object[] { """{ "name": "b5 a9 a5", "initial": { "pc": 52793, "s": 123, "a": 155, "x": 32, "y": 76, "p": 227, "ram": [ [52793, 181], [52794, 169], [52795, 165], [169, 165], [201, 176]]}, "final": { "pc": 52795, "s": 123, "a": 176, "x": 32, "y": 76, "p": 225, "ram": [ [169, 165], [201, 176], [52793, 181], [52794, 169], [52795, 165]]}, "cycles": [ [52793, 181, "read"], [52794, 169, "read"], [169, 165, "read"], [201, 176, "read"]] }""" };
	}

/*
    [Fact]
    public void ExecuteB1SampleTestCorrectly()
    {
        const string jsonText = """
{
	"name": "b1 28 b5",
	"initial": {
		"pc": 59082,
		"s": 39,
		"a": 57,
		"x": 33,
		"y": 174,
		"p": 96,
		"ram": [
			[59082, 177],
			[59083, 40],
			[59084, 181],
			[40, 160],
			[41, 233],
			[59982, 119]
		]
	},
	"final": {
		"pc": 59084,
		"s": 39,
		"a": 119,
		"x": 33,
		"y": 174,
		"p": 96,
		"ram": [
			[40, 160],
			[41, 233],
			[59082, 177],
			[59083, 40],
			[59084, 181],
			[59982, 119]
		]
	},
	"cycles": [
		[59082, 177, "read"],
		[59083, 40, "read"],
		[40, 160, "read"],
		[41, 233, "read"],
		[59083, 40, "read"],
		[59982, 119, "read"]
	]
}
""";

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        var data = JsonSerializer.Deserialize<SampleCpuTest>(jsonText, options);
		var trace = new List<(int, byte, string)>();
        var sut = new Cpu6502.Builder()
            .RunningProgram(new byte[] { 0xb1, 0x28 })
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
			Assert.Equal(data.Cycles[index][0].GetInt16(), trace[index].Item1);
			Assert.Equal(data.Cycles[index][1].GetByte(), trace[index].Item2);
			Assert.Equal(data.Cycles[index][2].GetString(), trace[index].Item3);
		}
    }*/

}