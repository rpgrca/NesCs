using NesCs.Common.Tests;

namespace NesCs.UnitTests.Cpu;

public class BranchInstructionsInCpuMust
{
    [Theory]
    [MemberData(nameof(OpcodeB0JsonFeeder))]
    [MemberData(nameof(OpcodeF0JsonFeeder))]
    public void BeExecutedCorrectly(SampleCpu sampleCpu)
    {
        var trace = new List<(int, byte, string)>();
        var sut = Utilities.CreateSubjectUnderTestFromSample(sampleCpu.Opcodes, sampleCpu.Initial, trace);
        sut.Run();

        Utilities.Equal(sampleCpu.Final, sut);
        Utilities.Equal(sampleCpu.Cycles, trace);
    }

    public static IEnumerable<object[]> OpcodeB0JsonFeeder()
    {
        /* no jump                       */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "b0 dc 98", "initial": { "pc": 20980, "s": 156, "a": 116, "x": 48, "y": 133, "p": 232, "ram": [ [20980, 176], [20981, 220], [20982, 152]]}, "final": { "pc": 20982, "s": 156, "a": 116, "x": 48, "y": 133, "p": 232, "ram": [ [20980, 176], [20981, 220], [20982, 152]]}, "cycles": [ [20980, 176, "read"], [20981, 220, "read"]] }""") };
        /* jump backward to same page    */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "b0 da cd", "initial": { "pc": 16337, "s": 156, "a": 188, "x": 155, "y": 109, "p": 175, "ram": [ [16337, 176], [16338, 218], [16339, 205], [16301, 146]]}, "final": { "pc": 16301, "s": 156, "a": 188, "x": 155, "y": 109, "p": 175, "ram": [ [16301, 146], [16337, 176], [16338, 218], [16339, 205]]}, "cycles": [ [16337, 176, "read"], [16338, 218, "read"], [16339, 205, "read"]] }""") };
        /* jump backward to another page */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "b0 bd ff", "initial": { "pc": 14080, "s": 3, "a": 1, "x": 249, "y": 62, "p": 173, "ram": [ [14080, 176], [14081, 189], [14082, 255], [14271, 6], [14015, 240]]}, "final": { "pc": 14015, "s": 3, "a": 1, "x": 249, "y": 62, "p": 173, "ram": [ [14015, 240], [14080, 176], [14081, 189], [14082, 255], [14271, 6]]}, "cycles": [ [14080, 176, "read"], [14081, 189, "read"], [14082, 255, "read"], [14271, 6, "read"]] }""") };
        /* jump forward to same page     */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "b0 20 8a", "initial": { "pc": 19652, "s": 57, "a": 75, "x": 4, "y": 80, "p": 233, "ram": [ [19652, 176], [19653, 32], [19654, 138], [19686, 44]]}, "final": { "pc": 19686, "s": 57, "a": 75, "x": 4, "y": 80, "p": 233, "ram": [ [19652, 176], [19653, 32], [19654, 138], [19686, 44]]}, "cycles": [ [19652, 176, "read"], [19653, 32, "read"], [19654, 138, "read"]] }""") };
        /* jump forward to another page  */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "b0 56 8b", "initial": { "pc": 434, "s": 18, "a": 243, "x": 85, "y": 26, "p": 97, "ram": [ [434, 176], [435, 86], [436, 139], [266, 98], [522, 93]]}, "final": { "pc": 522, "s": 18, "a": 243, "x": 85, "y": 26, "p": 97, "ram": [ [266, 98], [434, 176], [435, 86], [436, 139], [522, 93]]}, "cycles": [ [434, 176, "read"], [435, 86, "read"], [436, 139, "read"], [266, 98, "read"]] }""") };
    }

    public static IEnumerable<object[]> OpcodeF0JsonFeeder()
    {
        /* no jump                       */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "f0 b8 e3", "initial": { "pc": 62453, "s": 196, "a": 69, "x": 35, "y": 199, "p": 32, "ram": [ [62453, 240], [62454, 184], [62455, 227]]}, "final": { "pc": 62455, "s": 196, "a": 69, "x": 35, "y": 199, "p": 32, "ram": [ [62453, 240], [62454, 184], [62455, 227]]}, "cycles": [ [62453, 240, "read"], [62454, 184, "read"]] }""") };
        /* jump backward to another page */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "f0 a7 19", "initial": { "pc": 29268, "s": 59, "a": 0, "x": 185, "y": 180, "p": 163, "ram": [ [29268, 240], [29269, 167], [29270, 25], [29437, 180], [29181, 139]]}, "final": { "pc": 29181, "s": 59, "a": 0, "x": 185, "y": 180, "p": 163, "ram": [ [29181, 139], [29268, 240], [29269, 167], [29270, 25], [29437, 180]]}, "cycles": [ [29268, 240, "read"], [29269, 167, "read"], [29270, 25, "read"], [29437, 180, "read"]] }""") };
        /* jump forward to another page  */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "f0 7b 5c", "initial": { "pc": 30638, "s": 202, "a": 241, "x": 14, "y": 222, "p": 46, "ram": [ [30638, 240], [30639, 123], [30640, 92], [30507, 160], [30763, 223]]}, "final": { "pc": 30763, "s": 202, "a": 241, "x": 14, "y": 222, "p": 46, "ram": [ [30507, 160], [30638, 240], [30639, 123], [30640, 92], [30763, 223]]}, "cycles": [ [30638, 240, "read"], [30639, 123, "read"], [30640, 92, "read"], [30507, 160, "read"]] }""") };
        /* jump backward in same page    */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "f0 bb 37", "initial": { "pc": 19544, "s": 11, "a": 27, "x": 22, "y": 154, "p": 42, "ram": [ [19544, 240], [19545, 187], [19546, 55], [19477, 185]]}, "final": { "pc": 19477, "s": 11, "a": 27, "x": 22, "y": 154, "p": 42, "ram": [ [19477, 185], [19544, 240], [19545, 187], [19546, 55]]}, "cycles": [ [19544, 240, "read"], [19545, 187, "read"], [19546, 55, "read"]] }""") };
        /* negative pc                   */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "f0 8a d9", "initial": { "pc": 84, "s": 243, "a": 128, "x": 175, "y": 221, "p": 234, "ram": [ [84, 240], [85, 138], [86, 217], [224, 142], [65504, 202]]}, "final": { "pc": 65504, "s": 243, "a": 128, "x": 175, "y": 221, "p": 234, "ram": [ [84, 240], [85, 138], [86, 217], [224, 142], [65504, 202]]}, "cycles": [ [84, 240, "read"], [85, 138, "read"], [86, 217, "read"], [224, 142, "read"]] }""") };
    }
}