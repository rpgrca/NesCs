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


    [Theory]
	[MemberData(nameof(OpcodeB1JsonFeeder))]
    public void ExecuteB1SampleTestCorrectly(string jsonText)
    {
		var data = JsonSerializer.Deserialize<SampleCpuTest>(jsonText, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
		var trace = new List<(int, byte, string)>();
		var sut = Utilities.CreateSubjectUnderTestFromSample(data, trace);
		sut.Run();

		Utilities.Equal(data.Final, sut);
		Utilities.Equal(data.Cycles, trace);
    }

	public static IEnumerable<object[]> OpcodeB1JsonFeeder()
	{
		// Sample from README https://github.com/TomHarte/ProcessorTests/tree/main/nes6502
		yield return new object[] { """
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
""" };
		yield return new object[] { """{ "name": "b1 7c 80", "initial": { "pc": 13603, "s": 190, "a": 10, "x": 194, "y": 196, "p": 108, "ram": [ [13603, 177], [13604, 124], [13605, 128], [124, 82], [125, 205], [52502, 1], [52758, 18]]}, "final": { "pc": 13605, "s": 190, "a": 18, "x": 194, "y": 196, "p": 108, "ram": [ [124, 82], [125, 205], [13603, 177], [13604, 124], [13605, 128], [52502, 1], [52758, 18]]}, "cycles": [ [13603, 177, "read"], [13604, 124, "read"], [124, 82, "read"], [125, 205, "read"], [52502, 1, "read"], [52758, 18, "read"]] }""" };
	}
}