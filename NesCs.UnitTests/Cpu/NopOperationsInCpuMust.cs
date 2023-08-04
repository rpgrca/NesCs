using NesCs.Common.Tests;

namespace NesCs.UnitTests.Cpu;

public class NopOperationsInCpuMust
{
    [Theory]
    [MemberData(nameof(Opcode04JsonFeeder))]
    [MemberData(nameof(Opcode0CJsonFeeder))]
    [MemberData(nameof(Opcode44JsonFeeder))]
    [MemberData(nameof(Opcode64JsonFeeder))]
    [MemberData(nameof(OpcodeEAJsonFeeder))]
    public void BeExecutedCorrectly(SampleCpu sampleCpu)
    {
        var trace = new List<(int, byte, string)>();
        var sut = Utilities.CreateSubjectUnderTestFromSample(sampleCpu.Initial, trace);
        sut.Run();

        Utilities.Equal(sampleCpu.Final, sut);
        Utilities.Equal(sampleCpu.Cycles, trace);
    }

    public static IEnumerable<object[]> Opcode04JsonFeeder()
    {
        /*   0  */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "04 f5 1e", "initial": { "pc": 17311, "s": 29, "a": 215, "x": 111, "y": 27, "p": 103, "ram": [ [17311, 4], [17312, 245], [17313, 30], [245, 59]]}, "final": { "pc": 17313, "s": 29, "a": 215, "x": 111, "y": 27, "p": 103, "ram": [ [245, 59], [17311, 4], [17312, 245], [17313, 30]]}, "cycles": [ [17311, 4, "read"], [17312, 245, "read"], [245, 59, "read"]] }""") };
    }

    public static IEnumerable<object[]> Opcode0CJsonFeeder()
    {
        /*   0  */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "0c 86 22", "initial": { "pc": 14547, "s": 113, "a": 11, "x": 166, "y": 12, "p": 98, "ram": [ [14547, 12], [14548, 134], [14549, 34], [14550, 205]]}, "final": { "pc": 14550, "s": 113, "a": 11, "x": 166, "y": 12, "p": 98, "ram": [ [14547, 12], [14548, 134], [14549, 34], [14550, 205]]}, "cycles": [ [14547, 12, "read"], [14548, 134, "read"], [14549, 34, "read"]] }""") };
    }

    public static IEnumerable<object[]> Opcode44JsonFeeder()
    {
        /*   0  */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "44 f6 d5", "initial": { "pc": 4885, "s": 99, "a": 141, "x": 54, "y": 238, "p": 32, "ram": [ [4885, 68], [4886, 246], [4887, 213], [246, 249]]}, "final": { "pc": 4887, "s": 99, "a": 141, "x": 54, "y": 238, "p": 32, "ram": [ [246, 249], [4885, 68], [4886, 246], [4887, 213]]}, "cycles": [ [4885, 68, "read"], [4886, 246, "read"], [246, 249, "read"]] }""") };
    }

    public static IEnumerable<object[]> Opcode64JsonFeeder()
    {
        /*   0  */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "64 3a 84", "initial": { "pc": 53938, "s": 13, "a": 11, "x": 187, "y": 32, "p": 229, "ram": [ [53938, 100], [53939, 58], [53940, 132], [58, 185]]}, "final": { "pc": 53940, "s": 13, "a": 11, "x": 187, "y": 32, "p": 229, "ram": [ [58, 185], [53938, 100], [53939, 58], [53940, 132]]}, "cycles": [ [53938, 100, "read"], [53939, 58, "read"], [58, 185, "read"]] }""") };
    }

    public static IEnumerable<object[]> OpcodeEAJsonFeeder()
    {
        yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "ea 72 6d", "initial": { "pc": 5126, "s": 144, "a": 75, "x": 26, "y": 101, "p": 43, "ram": [ [5126, 234], [5127, 114], [5128, 109]]}, "final": { "pc": 5127, "s": 144, "a": 75, "x": 26, "y": 101, "p": 43, "ram": [ [5126, 234], [5127, 114], [5128, 109]]}, "cycles": [ [5126, 234, "read"], [5127, 114, "read"]] }""") };
        yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "ea 69 29", "initial": { "pc": 44011, "s": 132, "a": 181, "x": 235, "y": 198, "p": 102, "ram": [ [44011, 234], [44012, 105], [44013, 41]]}, "final": { "pc": 44012, "s": 132, "a": 181, "x": 235, "y": 198, "p": 102, "ram": [ [44011, 234], [44012, 105], [44013, 41]]}, "cycles": [ [44011, 234, "read"], [44012, 105, "read"]] }""") };
    }
}