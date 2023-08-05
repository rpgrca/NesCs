using NesCs.Common.Tests;

namespace NesCs.UnitTests.Cpu;

public class LogicalAndInCpuMust
{
	[Theory]
    [MemberData(nameof(Opcode21JsonFeeder))]
    [MemberData(nameof(Opcode25JsonFeeder))]
    [MemberData(nameof(Opcode29JsonFeeder))]
    [MemberData(nameof(Opcode2DJsonFeeder))]
    [MemberData(nameof(Opcode31JsonFeeder))]
    [MemberData(nameof(Opcode35JsonFeeder))]
    [MemberData(nameof(Opcode39JsonFeeder))]
    [MemberData(nameof(Opcode3DJsonFeeder))]
	public void BeExecutedCorrectly(SampleCpu sampleCpu)
    {
        var trace = new List<(int, byte, string)>();
        var sut = Utilities.CreateSubjectUnderTestFromSample(sampleCpu.Initial, trace);
        sut.Step();

        Utilities.Equal(sampleCpu.Final, sut);
        Utilities.Equal(sampleCpu.Cycles, trace);
    }

    public static IEnumerable<object[]> Opcode21JsonFeeder()
    {
        yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "21 b5 b2", "initial": { "pc": 62965, "s": 60, "a": 130, "x": 81, "y": 56, "p": 45, "ram": [ [62965, 33], [62966, 181], [62967, 178], [181, 28], [6, 43], [7, 252], [64555, 62]]}, "final": { "pc": 62967, "s": 60, "a": 2, "x": 81, "y": 56, "p": 45, "ram": [ [6, 43], [7, 252], [181, 28], [62965, 33], [62966, 181], [62967, 178], [64555, 62]]}, "cycles": [ [62965, 33, "read"], [62966, 181, "read"], [181, 28, "read"], [6, 43, "read"], [7, 252, "read"], [64555, 62, "read"]] }""") };
    }

    public static IEnumerable<object[]> Opcode25JsonFeeder()
    {
        yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "25 86 a1", "initial": { "pc": 16485, "s": 18, "a": 88, "x": 145, "y": 212, "p": 47, "ram": [ [16485, 37], [16486, 134], [16487, 161], [134, 86]]}, "final": { "pc": 16487, "s": 18, "a": 80, "x": 145, "y": 212, "p": 45, "ram": [ [134, 86], [16485, 37], [16486, 134], [16487, 161]]}, "cycles": [ [16485, 37, "read"], [16486, 134, "read"], [134, 86, "read"]] }""") };
    }

    public static IEnumerable<object[]> Opcode29JsonFeeder()
    {
        yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "29 98 42", "initial": { "pc": 18880, "s": 198, "a": 246, "x": 127, "y": 210, "p": 226, "ram": [ [18880, 41], [18881, 152], [18882, 66]]}, "final": { "pc": 18882, "s": 198, "a": 144, "x": 127, "y": 210, "p": 224, "ram": [ [18880, 41], [18881, 152], [18882, 66]]}, "cycles": [ [18880, 41, "read"], [18881, 152, "read"]] }""") };
    }

    public static IEnumerable<object[]> Opcode2DJsonFeeder()
    {
        yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "2d 13 74", "initial": { "pc": 22459, "s": 95, "a": 150, "x": 15, "y": 155, "p": 227, "ram": [ [22459, 45], [22460, 19], [22461, 116], [29715, 221], [22462, 26]]}, "final": { "pc": 22462, "s": 95, "a": 148, "x": 15, "y": 155, "p": 225, "ram": [ [22459, 45], [22460, 19], [22461, 116], [22462, 26], [29715, 221]]}, "cycles": [ [22459, 45, "read"], [22460, 19, "read"], [22461, 116, "read"], [29715, 221, "read"]] }""") };
    }

    public static IEnumerable<object[]> Opcode31JsonFeeder()
    {
        yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "31 21 40", "initial": { "pc": 574, "s": 181, "a": 140, "x": 205, "y": 150, "p": 100, "ram": [ [574, 49], [575, 33], [576, 64], [33, 235], [34, 126], [32385, 34], [32641, 136]]}, "final": { "pc": 576, "s": 181, "a": 136, "x": 205, "y": 150, "p": 228, "ram": [ [33, 235], [34, 126], [574, 49], [575, 33], [576, 64], [32385, 34], [32641, 136]]}, "cycles": [ [574, 49, "read"], [575, 33, "read"], [33, 235, "read"], [34, 126, "read"], [32385, 34, "read"], [32641, 136, "read"]] }""") };
    }

    public static IEnumerable<object[]> Opcode35JsonFeeder()
    {
        yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "35 ac 7c", "initial": { "pc": 39961, "s": 30, "a": 31, "x": 28, "y": 166, "p": 107, "ram": [ [39961, 53], [39962, 172], [39963, 124], [172, 212], [200, 229]]}, "final": { "pc": 39963, "s": 30, "a": 5, "x": 28, "y": 166, "p": 105, "ram": [ [172, 212], [200, 229], [39961, 53], [39962, 172], [39963, 124]]}, "cycles": [ [39961, 53, "read"], [39962, 172, "read"], [172, 212, "read"], [200, 229, "read"]] }""") };
    }

    public static IEnumerable<object[]> Opcode39JsonFeeder()
    {
        yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "39 ce 88", "initial": { "pc": 36813, "s": 25, "a": 62, "x": 102, "y": 79, "p": 38, "ram": [ [36813, 57], [36814, 206], [36815, 136], [34845, 125], [35101, 228], [36816, 43]]}, "final": { "pc": 36816, "s": 25, "a": 36, "x": 102, "y": 79, "p": 36, "ram": [ [34845, 125], [35101, 228], [36813, 57], [36814, 206], [36815, 136], [36816, 43]]}, "cycles": [ [36813, 57, "read"], [36814, 206, "read"], [36815, 136, "read"], [34845, 125, "read"], [35101, 228, "read"]] }""") };
    }

    public static IEnumerable<object[]> Opcode3DJsonFeeder()
    {
        yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "3d 18 f9", "initial": { "pc": 65080, "s": 38, "a": 118, "x": 255, "y": 142, "p": 106, "ram": [ [65080, 61], [65081, 24], [65082, 249], [63767, 162], [64023, 103], [65083, 65]]}, "final": { "pc": 65083, "s": 38, "a": 102, "x": 255, "y": 142, "p": 104, "ram": [ [63767, 162], [64023, 103], [65080, 61], [65081, 24], [65082, 249], [65083, 65]]}, "cycles": [ [65080, 61, "read"], [65081, 24, "read"], [65082, 249, "read"], [63767, 162, "read"], [64023, 103, "read"]] }""") };
    }
}