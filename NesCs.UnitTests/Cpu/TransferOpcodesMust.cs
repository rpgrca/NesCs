using NesCs.Tests.Common;

namespace NesCs.UnitTests.Cpu;

public class TransferOpcodesMust
{
	[Theory]
    [MemberData(nameof(OpcodeA8JsonFeeder))]
    [MemberData(nameof(OpcodeAAJsonFeeder))]
    [MemberData(nameof(OpcodeBAJsonFeeder))]
	public void BeExecutedCorrectly(SampleCpu sampleCpu)
    {
        var trace = new List<(int, byte, string)>();
        var sut = Utilities.CreateSubjectUnderTestFromSample(sampleCpu.Opcodes, sampleCpu.Initial, trace);
        sut.Run();

        Utilities.Equal(sampleCpu.Final, sut);
        Utilities.Equal(sampleCpu.Cycles, trace);
    }

    public static IEnumerable<object[]> OpcodeA8JsonFeeder()
    {
        yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "a8 d4 ac", "initial": { "pc": 23621, "s": 151, "a": 106, "x": 209, "y": 51, "p": 237, "ram": [ [23621, 168], [23622, 212], [23623, 172]]}, "final": { "pc": 23622, "s": 151, "a": 106, "x": 209, "y": 106, "p": 109, "ram": [ [23621, 168], [23622, 212], [23623, 172]]}, "cycles": [ [23621, 168, "read"], [23622, 212, "read"]] }""") };
    }

    public static IEnumerable<object[]> OpcodeAAJsonFeeder()
    {
        yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "aa cf 8d", "initial": { "pc": 584, "s": 112, "a": 191, "x": 113, "y": 195, "p": 44, "ram": [ [584, 170], [585, 207], [586, 141]]}, "final": { "pc": 585, "s": 112, "a": 191, "x": 191, "y": 195, "p": 172, "ram": [ [584, 170], [585, 207], [586, 141]]}, "cycles": [ [584, 170, "read"], [585, 207, "read"]] }""") };
    }

    public static IEnumerable<object[]> OpcodeBAJsonFeeder()
    {
        yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "ba 63 c3", "initial": { "pc": 909, "s": 163, "a": 0, "x": 69, "y": 218, "p": 235, "ram": [ [909, 186], [910, 99], [911, 195]]}, "final": { "pc": 910, "s": 163, "a": 0, "x": 163, "y": 218, "p": 233, "ram": [ [909, 186], [910, 99], [911, 195]]}, "cycles": [ [909, 186, "read"], [910, 99, "read"]] }""") };
    }

}