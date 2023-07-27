using System.Text.Json;
using NesCs.Tests.Common;

namespace NesCs.UnitTests;

public class Cpu6502ProcessorMust
{
    [Theory]
	[MemberData(nameof(OpcodeA5JsonFeeder))]
    public void ExecuteA5SampleTestCorrectly(string jsonText)
    {
		var data = JsonSerializer.Deserialize<SampleCpuTest>(jsonText, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
		var trace = new List<(int, byte, string)>();
		var sut = Utilities.CreateSubjectUnderTestFromSample(data, trace);
		sut.Run();

		Utilities.Equal(data.Final, sut);
		Utilities.Equal(data.Cycles, trace);
    }

	public static IEnumerable<object[]> OpcodeA5JsonFeeder()
	{
		yield return new object[] { """{ "name": "a5 78 f1", "initial": { "pc": 44571, "s": 149, "a": 49, "x": 224, "y": 25, "p": 110, "ram": [ [44571, 165], [44572, 120], [44573, 241], [120, 118]]}, "final": { "pc": 44573, "s": 149, "a": 118, "x": 224, "y": 25, "p": 108, "ram": [ [120, 118], [44571, 165], [44572, 120], [44573, 241]]}, "cycles": [ [44571, 165, "read"], [44572, 120, "read"], [120, 118, "read"]] }""" };
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
		yield return new object[] { """{ "name": "b1 7c 80", "initial": { "pc": 13603, "s": 190, "a": 10, "x": 194, "y": 196, "p": 108, "ram": [ [13603, 177], [13604, 124], [13605, 128], [124, 82], [125, 205], [52502, 1], [52758, 18]]}, "final": { "pc": 13605, "s": 190, "a": 18, "x": 194, "y": 196, "p": 108, "ram": [ [124, 82], [125, 205], [13603, 177], [13604, 124], [13605, 128], [52502, 1], [52758, 18]]}, "cycles": [ [13603, 177, "read"], [13604, 124, "read"], [124, 82, "read"], [125, 205, "read"], [52502, 1, "read"], [52758, 18, "read"]] }""" };        // common sample
		yield return new object[] { """{ "name": "b1 8f ba", "initial": { "pc": 5623, "s": 24, "a": 57, "x": 161, "y": 230, "p": 111, "ram": [ [5623, 177], [5624, 143], [5625, 186], [143, 237], [144, 136], [35027, 88], [35283, 89]]}, "final": { "pc": 5625, "s": 24, "a": 89, "x": 161, "y": 230, "p": 109, "ram": [ [143, 237], [144, 136], [5623, 177], [5624, 143], [5625, 186], [35027, 88], [35283, 89]]}, "cycles": [ [5623, 177, "read"], [5624, 143, "read"], [143, 237, "read"], [144, 136, "read"], [35027, 88, "read"], [35283, 89, "read"]] }""" };              // wrap around 256 sample
		yield return new object[] { """{ "name": "b1 96 e3", "initial": { "pc": 46214, "s": 250, "a": 105, "x": 244, "y": 129, "p": 43, "ram": [ [46214, 177], [46215, 150], [46216, 227], [150, 230], [151, 255], [65383, 109], [103, 180]]}, "final": { "pc": 46216, "s": 250, "a": 180, "x": 244, "y": 129, "p": 169, "ram": [ [103, 180], [150, 230], [151, 255], [46214, 177], [46215, 150], [46216, 227], [65383, 109]]}, "cycles": [ [46214, 177, "read"], [46215, 150, "read"], [150, 230, "read"], [151, 255, "read"], [65383, 109, "read"], [103, 180, "read"]] }""" }; // wrap around 65536 sample
		yield return new object[] { """{ "name": "b1 ff a1", "initial": { "pc": 53704, "s": 196, "a": 234, "x": 238, "y": 126, "p": 227, "ram": [ [53704, 177], [53705, 255], [53706, 161], [255, 80], [0, 201], [51662, 192]]}, "final": { "pc": 53706, "s": 196, "a": 192, "x": 238, "y": 126, "p": 225, "ram": [ [0, 201], [255, 80], [51662, 192], [53704, 177], [53705, 255], [53706, 161]]}, "cycles": [ [53704, 177, "read"], [53705, 255, "read"], [255, 80, "read"], [0, 201, "read"], [51662, 192, "read"]] }""" };                                                     // wrap around high byte address sample
	}

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
}