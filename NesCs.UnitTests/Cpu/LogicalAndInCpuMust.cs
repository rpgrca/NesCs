using NesCs.Tests.Common;

namespace NesCs.UnitTests.Cpu;

public class LogicalAndInCpuMust
{
	[Theory]
    [MemberData(nameof(Opcode29JsonFeeder))]
    [MemberData(nameof(Opcode25JsonFeeder))]
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

    public static IEnumerable<object[]> Opcode25JsonFeeder()
    {
        yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "25 86 a1", "initial": { "pc": 16485, "s": 18, "a": 88, "x": 145, "y": 212, "p": 47, "ram": [ [16485, 37], [16486, 134], [16487, 161], [134, 86]]}, "final": { "pc": 16487, "s": 18, "a": 80, "x": 145, "y": 212, "p": 45, "ram": [ [134, 86], [16485, 37], [16486, 134], [16487, 161]]}, "cycles": [ [16485, 37, "read"], [16486, 134, "read"], [134, 86, "read"]] }""") };
    }
}