using NesCs.Tests.Common;

namespace NesCs.UnitTests.Cpu;

public class LogicalAndInCpuMust
{
	[Theory]
    [MemberData(nameof(Opcode29JsonFeeder))]
	public void BeExecutedCorrectly(SampleCpu sampleCpu)
    {
        var trace = new List<(int, byte, string)>();
        var sut = Utilities.CreateSubjectUnderTestFromSample(sampleCpu.Opcodes, sampleCpu.Initial, trace);
        sut.Run();

        Utilities.Equal(sampleCpu.Final, sut);
        Utilities.Equal(sampleCpu.Cycles, trace);
    }

    public static IEnumerable<object[]> Opcode29JsonFeeder()
    {
        yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "29 98 42", "initial": { "pc": 18880, "s": 198, "a": 246, "x": 127, "y": 210, "p": 226, "ram": [ [18880, 41], [18881, 152], [18882, 66]]}, "final": { "pc": 18882, "s": 198, "a": 144, "x": 127, "y": 210, "p": 224, "ram": [ [18880, 41], [18881, 152], [18882, 66]]}, "cycles": [ [18880, 41, "read"], [18881, 152, "read"]] }""") };
    }
}