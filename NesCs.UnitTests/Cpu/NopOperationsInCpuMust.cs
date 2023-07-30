using NesCs.Common.Tests;

namespace NesCs.UnitTests.Cpu;

public class NopOperationsInCpuMust
{
    [Theory]
    [MemberData(nameof(OpcodeEAJsonFeeder))]
    public void BeExecutedCorrectly(SampleCpu sampleCpu)
    {
        var trace = new List<(int, byte, string)>();
        var sut = Utilities.CreateSubjectUnderTestFromSample(sampleCpu.Opcodes, sampleCpu.Initial, trace);
        sut.Run();

        Utilities.Equal(sampleCpu.Final, sut);
        Utilities.Equal(sampleCpu.Cycles, trace);
    }

    public static IEnumerable<object[]> OpcodeEAJsonFeeder()
    {
        yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "ea 72 6d", "initial": { "pc": 5126, "s": 144, "a": 75, "x": 26, "y": 101, "p": 43, "ram": [ [5126, 234], [5127, 114], [5128, 109]]}, "final": { "pc": 5127, "s": 144, "a": 75, "x": 26, "y": 101, "p": 43, "ram": [ [5126, 234], [5127, 114], [5128, 109]]}, "cycles": [ [5126, 234, "read"], [5127, 114, "read"]] }""") };
        yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "ea 69 29", "initial": { "pc": 44011, "s": 132, "a": 181, "x": 235, "y": 198, "p": 102, "ram": [ [44011, 234], [44012, 105], [44013, 41]]}, "final": { "pc": 44012, "s": 132, "a": 181, "x": 235, "y": 198, "p": 102, "ram": [ [44011, 234], [44012, 105], [44013, 41]]}, "cycles": [ [44011, 234, "read"], [44012, 105, "read"]] }""") };
    }
}