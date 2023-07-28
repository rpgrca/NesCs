using System.Text.Json;
using NesCs.Tests.Common;

namespace NesCs.UnitTests;

public class Cpu6502ProcessorMust
{
    [Theory]
	[MemberData(nameof(OpcodeA0JsonFeeder))]
	[MemberData(nameof(OpcodeA1JsonFeeder))]
	[MemberData(nameof(OpcodeA2JsonFeeder))]
	[MemberData(nameof(OpcodeA5JsonFeeder))]
	[MemberData(nameof(OpcodeA6JsonFeeder))]
	[MemberData(nameof(OpcodeA9JsonFeeder))]
	[MemberData(nameof(OpcodeADJsonFeeder))]
	[MemberData(nameof(OpcodeB1JsonFeeder))]
    [MemberData(nameof(OpcodeB5JsonFeeder))]
	[MemberData(nameof(OpcodeB9JsonFeeder))]
    [MemberData(nameof(OpcodeBDJsonFeeder))]
    public void ExecuteA5SampleTestCorrectly(int amount, string jsonText)
    {
		var data = JsonSerializer.Deserialize<SampleCpuTest>(jsonText, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
		var trace = new List<(int, byte, string)>();
		var sut = Utilities.CreateSubjectUnderTestFromSample(data, trace, amount);
		sut.Run();

		Utilities.Equal(data.Final, sut);
		Utilities.Equal(data.Cycles, trace);
    }

	public static IEnumerable<object[]> OpcodeA0JsonFeeder() =>
		new object[][] { new object[] { 2, """{ "name": "a0 3c 6e", "initial": { "pc": 16399, "s": 180, "a": 201, "x": 61, "y": 70, "p": 45, "ram": [ [16399, 160], [16400, 60], [16401, 110]]}, "final": { "pc": 16401, "s": 180, "a": 201, "x": 61, "y": 60, "p": 45, "ram": [ [16399, 160], [16400, 60], [16401, 110]]}, "cycles": [ [16399, 160, "read"], [16400, 60, "read"]] }""" } };

    public static IEnumerable<object[]> OpcodeA1JsonFeeder() =>
		new object[][] { new object[] { 2, """{ "name": "a1 e6 dd", "initial": { "pc": 52264, "s": 229, "a": 55, "x": 175, "y": 132, "p": 236, "ram": [ [52264, 161], [52265, 230], [52266, 221], [230, 178], [149, 154], [150, 85], [21914, 218]]}, "final": { "pc": 52266, "s": 229, "a": 218, "x": 175, "y": 132, "p": 236, "ram": [ [149, 154], [150, 85], [230, 178], [21914, 218], [52264, 161], [52265, 230], [52266, 221]]}, "cycles": [ [52264, 161, "read"], [52265, 230, "read"], [230, 178, "read"], [149, 154, "read"], [150, 85, "read"], [21914, 218, "read"]] }""" } };

	public static IEnumerable<object[]> OpcodeA2JsonFeeder() =>
		new object[][] { new object[] { 2, """{ "name": "a2 97 a1", "initial": { "pc": 56542, "s": 190, "a": 31, "x": 139, "y": 177, "p": 33, "ram": [ [56542, 162], [56543, 151], [56544, 161]]}, "final": { "pc": 56544, "s": 190, "a": 31, "x": 151, "y": 177, "p": 161, "ram": [ [56542, 162], [56543, 151], [56544, 161]]}, "cycles": [ [56542, 162, "read"], [56543, 151, "read"]] }""" } };

	public static IEnumerable<object[]> OpcodeA5JsonFeeder()
	{
		yield return new object[] { 2, """{ "name": "a5 78 f1", "initial": { "pc": 44571, "s": 149, "a": 49, "x": 224, "y": 25, "p": 110, "ram": [ [44571, 165], [44572, 120], [44573, 241], [120, 118]]}, "final": { "pc": 44573, "s": 149, "a": 118, "x": 224, "y": 25, "p": 108, "ram": [ [120, 118], [44571, 165], [44572, 120], [44573, 241]]}, "cycles": [ [44571, 165, "read"], [44572, 120, "read"], [120, 118, "read"]] }""" }; // common, validates 9999
		yield return new object[] { 2, """{ "name": "a5 46 e9", "initial": { "pc": 65534, "s": 198, "a": 122, "x": 73, "y": 75, "p": 109, "ram": [ [65534, 165], [65535, 70], [0, 233], [70, 6]]}, "final": { "pc": 0, "s": 198, "a": 6, "x": 73, "y": 75, "p": 109, "ram": [ [0, 233], [70, 6], [65534, 165], [65535, 70]]}, "cycles": [ [65534, 165, "read"], [65535, 70, "read"], [70, 6, "read"]] }""" }; // handle wrap around PC turning 0
	}

	public static IEnumerable<object[]> OpcodeA6JsonFeeder()
	{
		yield return new object[] { 2, """{ "name": "a6 ed bc", "initial": { "pc": 37163, "s": 182, "a": 193, "x": 104, "y": 243, "p": 103, "ram": [ [37163, 166], [37164, 237], [37165, 188], [237, 68]]}, "final": { "pc": 37165, "s": 182, "a": 193, "x": 68, "y": 243, "p": 101, "ram": [ [237, 68], [37163, 166], [37164, 237], [37165, 188]]}, "cycles": [ [37163, 166, "read"], [37164, 237, "read"], [237, 68, "read"]] }""" };
	}

	public static IEnumerable<object[]> OpcodeA9JsonFeeder()
	{
		yield return new object[] { 2, """{ "name": "a9 c3 7a", "initial": { "pc": 33710, "s": 215, "a": 22, "x": 214, "y": 9, "p": 162, "ram": [ [33710, 169], [33711, 195], [33712, 122]]}, "final": { "pc": 33712, "s": 215, "a": 195, "x": 214, "y": 9, "p": 160, "ram": [ [33710, 169], [33711, 195], [33712, 122]]}, "cycles": [ [33710, 169, "read"], [33711, 195, "read"]] }""" }; // common
	}

	public static IEnumerable<object[]> OpcodeADJsonFeeder()
	{
		yield return new object[] { 3, """{ "name": "ad 19 5f", "initial": { "pc": 29984, "s": 178, "a": 200, "x": 197, "y": 56, "p": 226, "ram": [ [29984, 173], [29985, 25], [29986, 95], [24345, 36], [29987, 41]]}, "final": { "pc": 29987, "s": 178, "a": 36, "x": 197, "y": 56, "p": 96, "ram": [ [24345, 36], [29984, 173], [29985, 25], [29986, 95], [29987, 41]]}, "cycles": [ [29984, 173, "read"], [29985, 25, "read"], [29986, 95, "read"], [24345, 36, "read"]] }""" };
	}

	public static IEnumerable<object[]> OpcodeB1JsonFeeder()
	{
		yield return new object[] { 2, """{ "name": "b1 7c 80", "initial": { "pc": 13603, "s": 190, "a": 10, "x": 194, "y": 196, "p": 108, "ram": [ [13603, 177], [13604, 124], [13605, 128], [124, 82], [125, 205], [52502, 1], [52758, 18]]}, "final": { "pc": 13605, "s": 190, "a": 18, "x": 194, "y": 196, "p": 108, "ram": [ [124, 82], [125, 205], [13603, 177], [13604, 124], [13605, 128], [52502, 1], [52758, 18]]}, "cycles": [ [13603, 177, "read"], [13604, 124, "read"], [124, 82, "read"], [125, 205, "read"], [52502, 1, "read"], [52758, 18, "read"]] }""" };        // common sample
		yield return new object[] { 2, """{ "name": "b1 8f ba", "initial": { "pc": 5623, "s": 24, "a": 57, "x": 161, "y": 230, "p": 111, "ram": [ [5623, 177], [5624, 143], [5625, 186], [143, 237], [144, 136], [35027, 88], [35283, 89]]}, "final": { "pc": 5625, "s": 24, "a": 89, "x": 161, "y": 230, "p": 109, "ram": [ [143, 237], [144, 136], [5623, 177], [5624, 143], [5625, 186], [35027, 88], [35283, 89]]}, "cycles": [ [5623, 177, "read"], [5624, 143, "read"], [143, 237, "read"], [144, 136, "read"], [35027, 88, "read"], [35283, 89, "read"]] }""" };              // wrap around 256 sample
		yield return new object[] { 2, """{ "name": "b1 96 e3", "initial": { "pc": 46214, "s": 250, "a": 105, "x": 244, "y": 129, "p": 43, "ram": [ [46214, 177], [46215, 150], [46216, 227], [150, 230], [151, 255], [65383, 109], [103, 180]]}, "final": { "pc": 46216, "s": 250, "a": 180, "x": 244, "y": 129, "p": 169, "ram": [ [103, 180], [150, 230], [151, 255], [46214, 177], [46215, 150], [46216, 227], [65383, 109]]}, "cycles": [ [46214, 177, "read"], [46215, 150, "read"], [150, 230, "read"], [151, 255, "read"], [65383, 109, "read"], [103, 180, "read"]] }""" }; // wrap around 65536 sample
		yield return new object[] { 2, """{ "name": "b1 ff a1", "initial": { "pc": 53704, "s": 196, "a": 234, "x": 238, "y": 126, "p": 227, "ram": [ [53704, 177], [53705, 255], [53706, 161], [255, 80], [0, 201], [51662, 192]]}, "final": { "pc": 53706, "s": 196, "a": 192, "x": 238, "y": 126, "p": 225, "ram": [ [0, 201], [255, 80], [51662, 192], [53704, 177], [53705, 255], [53706, 161]]}, "cycles": [ [53704, 177, "read"], [53705, 255, "read"], [255, 80, "read"], [0, 201, "read"], [51662, 192, "read"]] }""" };                                                     // wrap around high byte address sample
	}

	public static IEnumerable<object[]> OpcodeB5JsonFeeder()
	{
		yield return new object[] { 2, """
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
		yield return new object[] { 2, """{ "name": "b5 a9 a5", "initial": { "pc": 52793, "s": 123, "a": 155, "x": 32, "y": 76, "p": 227, "ram": [ [52793, 181], [52794, 169], [52795, 165], [169, 165], [201, 176]]}, "final": { "pc": 52795, "s": 123, "a": 176, "x": 32, "y": 76, "p": 225, "ram": [ [169, 165], [201, 176], [52793, 181], [52794, 169], [52795, 165]]}, "cycles": [ [52793, 181, "read"], [52794, 169, "read"], [169, 165, "read"], [201, 176, "read"]] }""" };
	}

	public static IEnumerable<object[]> OpcodeB9JsonFeeder()
	{
		yield return new object[] { 3, """{ "name": "b9 bc b9", "initial": { "pc": 57299, "s": 12, "a": 148, "x": 64, "y": 42, "p": 171, "ram": [ [57299, 185], [57300, 188], [57301, 185], [47590, 199], [57302, 15]]}, "final": { "pc": 57302, "s": 12, "a": 199, "x": 64, "y": 42, "p": 169, "ram": [ [47590, 199], [57299, 185], [57300, 188], [57301, 185], [57302, 15]]}, "cycles": [ [57299, 185, "read"], [57300, 188, "read"], [57301, 185, "read"], [47590, 199, "read"]] }""" };
	}

	public static IEnumerable<object[]> OpcodeBDJsonFeeder()
	{
		yield return new object[] { 3, """{ "name": "bd 48 17", "initial": { "pc": 9514, "s": 243, "a": 49, "x": 96, "y": 94, "p": 38, "ram": [ [9514, 189], [9515, 72], [9516, 23], [6056, 41], [9517, 67]]}, "final": { "pc": 9517, "s": 243, "a": 41, "x": 96, "y": 94, "p": 36, "ram": [ [6056, 41], [9514, 189], [9515, 72], [9516, 23], [9517, 67]]}, "cycles": [ [9514, 189, "read"], [9515, 72, "read"], [9516, 23, "read"], [6056, 41, "read"]] }""" };
		yield return new object[] { 3, """{ "name": "bd 98 2c", "initial": { "pc": 32131, "s": 64, "a": 168, "x": 213, "y": 216, "p": 236, "ram": [ [32131, 189], [32132, 152], [32133, 44], [11373, 46], [11629, 15], [32134, 153]]}, "final": { "pc": 32134, "s": 64, "a": 15, "x": 213, "y": 216, "p": 108, "ram": [ [11373, 46], [11629, 15], [32131, 189], [32132, 152], [32133, 44], [32134, 153]]}, "cycles": [ [32131, 189, "read"], [32132, 152, "read"], [32133, 44, "read"], [11373, 46, "read"], [11629, 15, "read"]] }""" }; // wrap low byte
		yield return new object[] { 3, """{ "name": "bd b3 ff", "initial": { "pc": 20035, "s": 251, "a": 200, "x": 131, "y": 115, "p": 229, "ram": [ [20035, 189], [20036, 179], [20037, 255], [65334, 231], [54, 9], [20038, 105]]}, "final": { "pc": 20038, "s": 251, "a": 9, "x": 131, "y": 115, "p": 101, "ram": [ [54, 9], [20035, 189], [20036, 179], [20037, 255], [20038, 105], [65334, 231]]}, "cycles": [ [20035, 189, "read"], [20036, 179, "read"], [20037, 255, "read"], [65334, 231, "read"], [54, 9, "read"]] }""" };
	}
}