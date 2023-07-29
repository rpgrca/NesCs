using NesCs.Tests.Common;

namespace NesCs.UnitTests.Cpu;

public class LogicalAndInCpuMust
{
	[Theory]
    [MemberData(nameof(Opcode25JsonFeeder))]
    [MemberData(nameof(Opcode29JsonFeeder))]
    [MemberData(nameof(Opcode2DJsonFeeder))]
    [MemberData(nameof(Opcode35JsonFeeder))]
	public void BeExecutedCorrectly(SampleCpu sampleCpu)
    {
        var trace = new List<(int, byte, string)>();
        var sut = Utilities.CreateSubjectUnderTestFromSample(sampleCpu.Opcodes, sampleCpu.Initial, trace);
        sut.Run();

        Utilities.Equal(sampleCpu.Final, sut);
        Utilities.Equal(sampleCpu.Cycles, trace);
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

     public static IEnumerable<object[]> Opcode35JsonFeeder()
    {
        yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "35 ac 7c", "initial": { "pc": 39961, "s": 30, "a": 31, "x": 28, "y": 166, "p": 107, "ram": [ [39961, 53], [39962, 172], [39963, 124], [172, 212], [200, 229]]}, "final": { "pc": 39963, "s": 30, "a": 5, "x": 28, "y": 166, "p": 105, "ram": [ [172, 212], [200, 229], [39961, 53], [39962, 172], [39963, 124]]}, "cycles": [ [39961, 53, "read"], [39962, 172, "read"], [172, 212, "read"], [200, 229, "read"]] }""") };
    }


}