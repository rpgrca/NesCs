using System.Text.Json;
using NesCs.Tests.Common;

namespace NesCs.UnitTests.Cpu;

public class LogicalInclusiveOrInCpuMust
{
	[Theory]
	[MemberData(nameof(Opcode09JsonFeeder))]
	public void BeExecutedCorrectly(string jsonText)
	{
        var data = JsonSerializer.Deserialize<SampleCpuTest>(jsonText, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        var trace = new List<(int, byte, string)>();
        var sut = Utilities.CreateSubjectUnderTestFromSample(data, trace);
        sut.Run();

        Utilities.Equal(data.Final, sut);
        Utilities.Equal(data.Cycles, trace);
	}

    public static IEnumerable<object[]> Opcode09JsonFeeder()
    {
        //yield return new object[] { """{ "name": "09 0a d8", "initial": { "pc": 50994, "s": 230, "a": 187, "x": 65, "y": 111, "p": 172, "ram": [ [50994, 9], [50995, 10], [50996, 216]]}, "final": { "pc": 50996, "s": 230, "a": 187, "x": 65, "y": 111, "p": 172, "ram": [ [50994, 9], [50995, 10], [50996, 216]]}, "cycles": [ [50994, 9, "read"], [50995, 10, "read"]] }""" }; // common
        //yield return new object[] { """{ "name": "09 e7 6d", "initial": { "pc": 43532, "s": 53, "a": 247, "x": 110, "y": 120, "p": 230, "ram": [ [43532, 9], [43533, 231], [43534, 109]]}, "final": { "pc": 43534, "s": 53, "a": 247, "x": 110, "y": 120, "p": 228, "ram": [ [43532, 9], [43533, 231], [43534, 109]]}, "cycles": [ [43532, 9, "read"], [43533, 231, "read"]] }""" }; // zero flag set sample
        yield return new object[] { """{ "name": "09 da ad", "initial": { "pc": 44847, "s": 201, "a": 33, "x": 97, "y": 103, "p": 44, "ram": [ [44847, 9], [44848, 218], [44849, 173]]}, "final": { "pc": 44849, "s": 201, "a": 251, "x": 97, "y": 103, "p": 172, "ram": [ [44847, 9], [44848, 218], [44849, 173]]}, "cycles": [ [44847, 9, "read"], [44848, 218, "read"]] }""" };
    }
}