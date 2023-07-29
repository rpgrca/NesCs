using NesCs.Tests.Common;

namespace NesCs.UnitTests.Cpu;

public class TransferOpcodesMust
{
	[Theory]
    [MemberData(nameof(OpcodeAAJsonFeeder))]
	public void BeExecutedCorrectly(SampleCpu sampleCpu)
    {
        var trace = new List<(int, byte, string)>();
        var sut = Utilities.CreateSubjectUnderTestFromSample(sampleCpu.Opcodes, sampleCpu.Initial, trace);
        sut.Run();

        Utilities.Equal(sampleCpu.Final, sut);
        Utilities.Equal(sampleCpu.Cycles, trace);
    }

    public static IEnumerable<object[]> OpcodeAAJsonFeeder()
    {
        yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "aa cf 8d", "initial": { "pc": 584, "s": 112, "a": 191, "x": 113, "y": 195, "p": 44, "ram": [ [584, 170], [585, 207], [586, 141]]}, "final": { "pc": 585, "s": 112, "a": 191, "x": 191, "y": 195, "p": 172, "ram": [ [584, 170], [585, 207], [586, 141]]}, "cycles": [ [584, 170, "read"], [585, 207, "read"]] }""") };
    }
}