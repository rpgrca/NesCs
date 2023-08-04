using NesCs.Common.Tests;

namespace NesCs.UnitTests.Cpu;

public class NopOperationsInCpuMust
{
    [Theory]
    [MemberData(nameof(Opcode04JsonFeeder))]
    [MemberData(nameof(Opcode0CJsonFeeder))]
    [MemberData(nameof(Opcode14JsonFeeder))]
    [MemberData(nameof(Opcode1AJsonFeeder))]
    [MemberData(nameof(Opcode34JsonFeeder))]
    [MemberData(nameof(Opcode44JsonFeeder))]
    [MemberData(nameof(Opcode54JsonFeeder))]
    [MemberData(nameof(Opcode64JsonFeeder))]
    [MemberData(nameof(Opcode74JsonFeeder))]
    [MemberData(nameof(OpcodeD4JsonFeeder))]
    [MemberData(nameof(OpcodeEAJsonFeeder))]
    [MemberData(nameof(OpcodeF4JsonFeeder))]
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

    public static IEnumerable<object[]> Opcode14JsonFeeder()
    {
        /*   0  */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "14 65 be", "initial": { "pc": 41348, "s": 207, "a": 139, "x": 9, "y": 204, "p": 38, "ram": [ [41348, 20], [41349, 101], [41350, 190], [101, 12], [110, 56]]}, "final": { "pc": 41350, "s": 207, "a": 139, "x": 9, "y": 204, "p": 38, "ram": [ [101, 12], [110, 56], [41348, 20], [41349, 101], [41350, 190]]}, "cycles": [ [41348, 20, "read"], [41349, 101, "read"], [101, 12, "read"], [110, 56, "read"]] }""") };
    }

    public static IEnumerable<object[]> Opcode1AJsonFeeder()
    {
        /*   0  */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "1a 66 12", "initial": { "pc": 52409, "s": 45, "a": 165, "x": 165, "y": 152, "p": 236, "ram": [ [52409, 26], [52410, 102], [52411, 18]]}, "final": { "pc": 52410, "s": 45, "a": 165, "x": 165, "y": 152, "p": 236, "ram": [ [52409, 26], [52410, 102], [52411, 18]]}, "cycles": [ [52409, 26, "read"], [52410, 102, "read"]] }""") };
    }

    public static IEnumerable<object[]> Opcode34JsonFeeder()
    {
        /*   0  */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "34 94 6f", "initial": { "pc": 28616, "s": 11, "a": 204, "x": 31, "y": 62, "p": 167, "ram": [ [28616, 52], [28617, 148], [28618, 111], [148, 147], [179, 160]]}, "final": { "pc": 28618, "s": 11, "a": 204, "x": 31, "y": 62, "p": 167, "ram": [ [148, 147], [179, 160], [28616, 52], [28617, 148], [28618, 111]]}, "cycles": [ [28616, 52, "read"], [28617, 148, "read"], [148, 147, "read"], [179, 160, "read"]] }""") };
    }

    public static IEnumerable<object[]> Opcode44JsonFeeder()
    {
        /*   0  */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "44 f6 d5", "initial": { "pc": 4885, "s": 99, "a": 141, "x": 54, "y": 238, "p": 32, "ram": [ [4885, 68], [4886, 246], [4887, 213], [246, 249]]}, "final": { "pc": 4887, "s": 99, "a": 141, "x": 54, "y": 238, "p": 32, "ram": [ [246, 249], [4885, 68], [4886, 246], [4887, 213]]}, "cycles": [ [4885, 68, "read"], [4886, 246, "read"], [246, 249, "read"]] }""") };
    }

    public static IEnumerable<object[]> Opcode54JsonFeeder()
    {
        /*   0  */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "54 69 95", "initial": { "pc": 8996, "s": 166, "a": 137, "x": 116, "y": 29, "p": 234, "ram": [ [8996, 84], [8997, 105], [8998, 149], [105, 100], [221, 19]]}, "final": { "pc": 8998, "s": 166, "a": 137, "x": 116, "y": 29, "p": 234, "ram": [ [105, 100], [221, 19], [8996, 84], [8997, 105], [8998, 149]]}, "cycles": [ [8996, 84, "read"], [8997, 105, "read"], [105, 100, "read"], [221, 19, "read"]] }""") };
    }

    public static IEnumerable<object[]> Opcode64JsonFeeder()
    {
        /*   0  */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "64 3a 84", "initial": { "pc": 53938, "s": 13, "a": 11, "x": 187, "y": 32, "p": 229, "ram": [ [53938, 100], [53939, 58], [53940, 132], [58, 185]]}, "final": { "pc": 53940, "s": 13, "a": 11, "x": 187, "y": 32, "p": 229, "ram": [ [58, 185], [53938, 100], [53939, 58], [53940, 132]]}, "cycles": [ [53938, 100, "read"], [53939, 58, "read"], [58, 185, "read"]] }""") };
    }

    public static IEnumerable<object[]> Opcode74JsonFeeder()
    {
        /*   0  */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "74 57 22", "initial": { "pc": 65151, "s": 66, "a": 89, "x": 115, "y": 240, "p": 234, "ram": [ [65151, 116], [65152, 87], [65153, 34], [87, 241], [202, 44]]}, "final": { "pc": 65153, "s": 66, "a": 89, "x": 115, "y": 240, "p": 234, "ram": [ [87, 241], [202, 44], [65151, 116], [65152, 87], [65153, 34]]}, "cycles": [ [65151, 116, "read"], [65152, 87, "read"], [87, 241, "read"], [202, 44, "read"]] }""") };
    }

    public static IEnumerable<object[]> OpcodeD4JsonFeeder()
    {
        /*   0  */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "d4 b8 5d", "initial": { "pc": 56213, "s": 3, "a": 160, "x": 189, "y": 128, "p": 168, "ram": [ [56213, 212], [56214, 184], [56215, 93], [184, 4], [117, 217]]}, "final": { "pc": 56215, "s": 3, "a": 160, "x": 189, "y": 128, "p": 168, "ram": [ [117, 217], [184, 4], [56213, 212], [56214, 184], [56215, 93]]}, "cycles": [ [56213, 212, "read"], [56214, 184, "read"], [184, 4, "read"], [117, 217, "read"]] }""") };
    }

    public static IEnumerable<object[]> OpcodeEAJsonFeeder()
    {
        yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "ea 72 6d", "initial": { "pc": 5126, "s": 144, "a": 75, "x": 26, "y": 101, "p": 43, "ram": [ [5126, 234], [5127, 114], [5128, 109]]}, "final": { "pc": 5127, "s": 144, "a": 75, "x": 26, "y": 101, "p": 43, "ram": [ [5126, 234], [5127, 114], [5128, 109]]}, "cycles": [ [5126, 234, "read"], [5127, 114, "read"]] }""") };
        yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "ea 69 29", "initial": { "pc": 44011, "s": 132, "a": 181, "x": 235, "y": 198, "p": 102, "ram": [ [44011, 234], [44012, 105], [44013, 41]]}, "final": { "pc": 44012, "s": 132, "a": 181, "x": 235, "y": 198, "p": 102, "ram": [ [44011, 234], [44012, 105], [44013, 41]]}, "cycles": [ [44011, 234, "read"], [44012, 105, "read"]] }""") };
    }

    public static IEnumerable<object[]> OpcodeF4JsonFeeder()
    {
        /*   0  */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "f4 d2 2e", "initial": { "pc": 28644, "s": 116, "a": 3, "x": 210, "y": 223, "p": 99, "ram": [ [28644, 244], [28645, 210], [28646, 46], [210, 216], [164, 244]]}, "final": { "pc": 28646, "s": 116, "a": 3, "x": 210, "y": 223, "p": 99, "ram": [ [164, 244], [210, 216], [28644, 244], [28645, 210], [28646, 46]]}, "cycles": [ [28644, 244, "read"], [28645, 210, "read"], [210, 216, "read"], [164, 244, "read"]] }""") };
    }
}