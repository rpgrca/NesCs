using System.Text.Json;
using NesCs.Tests.Common;

namespace NesCs.UnitTests.Cpu;

public class LogicalInclusiveOrInCpuMust
{
	[Theory]
    [MemberData(nameof(Opcode05JsonFeeder))]
	[MemberData(nameof(Opcode09JsonFeeder))]
    [MemberData(nameof(Opcode0DJsonFeeder))]
	[MemberData(nameof(Opcode15JsonFeeder))]
    [MemberData(nameof(Opcode19JsonFeeder))]
	[MemberData(nameof(Opcode1DJsonFeeder))]
	public void BeExecutedCorrectly(string jsonText)
	{
        var data = JsonSerializer.Deserialize<SampleCpuTest>(jsonText, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        var trace = new List<(int, byte, string)>();
        var sut = Utilities.CreateSubjectUnderTestFromSample(data, trace);
        sut.Run();

        Utilities.Equal(data.Final, sut);
        Utilities.Equal(data.Cycles, trace);
	}

    public static IEnumerable<object[]> Opcode05JsonFeeder() =>
        new object[][] { new object[] { """{ "name": "05 65 9c", "initial": { "pc": 3097, "s": 200, "a": 209, "x": 135, "y": 48, "p": 232, "ram": [ [3097, 5], [3098, 101], [3099, 156], [101, 45]]}, "final": { "pc": 3099, "s": 200, "a": 253, "x": 135, "y": 48, "p": 232, "ram": [ [101, 45], [3097, 5], [3098, 101], [3099, 156]]}, "cycles": [ [3097, 5, "read"], [3098, 101, "read"], [101, 45, "read"]] }""" } };

    public static IEnumerable<object[]> Opcode09JsonFeeder()
    {
        yield return new object[] { """{ "name": "09 0a d8", "initial": { "pc": 50994, "s": 230, "a": 187, "x": 65, "y": 111, "p": 172, "ram": [ [50994, 9], [50995, 10], [50996, 216]]}, "final": { "pc": 50996, "s": 230, "a": 187, "x": 65, "y": 111, "p": 172, "ram": [ [50994, 9], [50995, 10], [50996, 216]]}, "cycles": [ [50994, 9, "read"], [50995, 10, "read"]] }""" }; // common
        yield return new object[] { """{ "name": "09 e7 6d", "initial": { "pc": 43532, "s": 53, "a": 247, "x": 110, "y": 120, "p": 230, "ram": [ [43532, 9], [43533, 231], [43534, 109]]}, "final": { "pc": 43534, "s": 53, "a": 247, "x": 110, "y": 120, "p": 228, "ram": [ [43532, 9], [43533, 231], [43534, 109]]}, "cycles": [ [43532, 9, "read"], [43533, 231, "read"]] }""" }; // zero flag set sample
        yield return new object[] { """{ "name": "09 da ad", "initial": { "pc": 44847, "s": 201, "a": 33, "x": 97, "y": 103, "p": 44, "ram": [ [44847, 9], [44848, 218], [44849, 173]]}, "final": { "pc": 44849, "s": 201, "a": 251, "x": 97, "y": 103, "p": 172, "ram": [ [44847, 9], [44848, 218], [44849, 173]]}, "cycles": [ [44847, 9, "read"], [44848, 218, "read"]] }""" };
    }

    public static IEnumerable<object[]> Opcode0DJsonFeeder() =>
        new object[][] { new object[] { """{ "name": "0d cb fd", "initial": { "pc": 29671, "s": 57, "a": 253, "x": 36, "y": 9, "p": 37, "ram": [ [29671, 13], [29672, 203], [29673, 253], [64971, 245], [29674, 10]]}, "final": { "pc": 29674, "s": 57, "a": 253, "x": 36, "y": 9, "p": 165, "ram": [ [29671, 13], [29672, 203], [29673, 253], [29674, 10], [64971, 245]]}, "cycles": [ [29671, 13, "read"], [29672, 203, "read"], [29673, 253, "read"], [64971, 245, "read"]] }""" } };

    public static IEnumerable<object[]> Opcode15JsonFeeder() =>
        new object[][] { new object[] { """{ "name": "15 86 a4", "initial": { "pc": 54719, "s": 18, "a": 35, "x": 142, "y": 223, "p": 229, "ram": [ [54719, 21], [54720, 134], [54721, 164], [134, 232], [20, 252]]}, "final": { "pc": 54721, "s": 18, "a": 255, "x": 142, "y": 223, "p": 229, "ram": [ [20, 252], [134, 232], [54719, 21], [54720, 134], [54721, 164]]}, "cycles": [ [54719, 21, "read"], [54720, 134, "read"], [134, 232, "read"], [20, 252, "read"]] }""" } };

    public static IEnumerable<object[]> Opcode19JsonFeeder() =>
        new object[][] { new object[] { """{ "name": "19 30 0f", "initial": { "pc": 57229, "s": 158, "a": 140, "x": 226, "y": 244, "p": 172, "ram": [ [57229, 25], [57230, 48], [57231, 15], [3876, 216], [4132, 136], [57232, 203]]}, "final": { "pc": 57232, "s": 158, "a": 140, "x": 226, "y": 244, "p": 172, "ram": [ [3876, 216], [4132, 136], [57229, 25], [57230, 48], [57231, 15], [57232, 203]]}, "cycles": [ [57229, 25, "read"], [57230, 48, "read"], [57231, 15, "read"], [3876, 216, "read"], [4132, 136, "read"]] }""" } };

    public static IEnumerable<object[]> Opcode1DJsonFeeder() =>
        new object[][] { new object[] { """{ "name": "1d 7a 25", "initial": { "pc": 34101, "s": 242, "a": 163, "x": 172, "y": 93, "p": 99, "ram": [ [34101, 29], [34102, 122], [34103, 37], [9510, 142], [9766, 96], [34104, 168]]}, "final": { "pc": 34104, "s": 242, "a": 227, "x": 172, "y": 93, "p": 225, "ram": [ [9510, 142], [9766, 96], [34101, 29], [34102, 122], [34103, 37], [34104, 168]]}, "cycles": [ [34101, 29, "read"], [34102, 122, "read"], [34103, 37, "read"], [9510, 142, "read"], [9766, 96, "read"]] }""" } };

}