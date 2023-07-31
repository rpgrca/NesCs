using NesCs.Common.Tests;

namespace NesCs.UnitTests.Cpu;

public class BranchInstructionsInCpuMust
{
    [Theory]
    [MemberData(nameof(OpcodeF0JsonFeeder))]
    public void BeExecutedCorrectly(SampleCpu sampleCpu)
    {
        var trace = new List<(int, byte, string)>();
        var sut = Utilities.CreateSubjectUnderTestFromSample(sampleCpu.Opcodes, sampleCpu.Initial, trace);
        sut.Run();

        Utilities.Equal(sampleCpu.Final, sut);
        Utilities.Equal(sampleCpu.Cycles, trace);
    }

    public static IEnumerable<object[]> OpcodeF0JsonFeeder()
    {
        /* no jump             */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "f0 b8 e3", "initial": { "pc": 62453, "s": 196, "a": 69, "x": 35, "y": 199, "p": 32, "ram": [ [62453, 240], [62454, 184], [62455, 227]]}, "final": { "pc": 62455, "s": 196, "a": 69, "x": 35, "y": 199, "p": 32, "ram": [ [62453, 240], [62454, 184], [62455, 227]]}, "cycles": [ [62453, 240, "read"], [62454, 184, "read"]] }""") };
        ///* large jump forward  */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "f0 7b 5c", "initial": { "pc": 30638, "s": 202, "a": 241, "x": 14, "y": 222, "p": 46, "ram": [ [30638, 240], [30639, 123], [30640, 92], [30507, 160], [30763, 223]]}, "final": { "pc": 30763, "s": 202, "a": 241, "x": 14, "y": 222, "p": 46, "ram": [ [30507, 160], [30638, 240], [30639, 123], [30640, 92], [30763, 223]]}, "cycles": [ [30638, 240, "read"], [30639, 123, "read"], [30640, 92, "read"], [30507, 160, "read"]] }""") };
        ///* large jump backward */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "f0 bb 37", "initial": { "pc": 19544, "s": 11, "a": 27, "x": 22, "y": 154, "p": 42, "ram": [ [19544, 240], [19545, 187], [19546, 55], [19477, 185]]}, "final": { "pc": 19477, "s": 11, "a": 27, "x": 22, "y": 154, "p": 42, "ram": [ [19477, 185], [19544, 240], [19545, 187], [19546, 55]]}, "cycles": [ [19544, 240, "read"], [19545, 187, "read"], [19546, 55, "read"]] }""") };
    }
}