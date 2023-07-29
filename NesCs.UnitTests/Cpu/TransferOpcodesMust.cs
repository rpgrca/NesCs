using NesCs.Tests.Common;

namespace NesCs.UnitTests.Cpu;

public class TransferOpcodesMust
{
	[Theory]
    [MemberData(nameof(Opcode8AJsonFeeder))]
    [MemberData(nameof(Opcode9AJsonFeeder))]
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

    public static IEnumerable<object[]> Opcode8AJsonFeeder()
    {
        yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "8a cb b2", "initial": { "pc": 4244, "s": 33, "a": 154, "x": 54, "y": 246, "p": 42, "ram": [ [4244, 138], [4245, 203], [4246, 178]]}, "final": { "pc": 4245, "s": 33, "a": 54, "x": 54, "y": 246, "p": 40, "ram": [ [4244, 138], [4245, 203], [4246, 178]]}, "cycles": [ [4244, 138, "read"], [4245, 203, "read"]] }""") };
    }

    public static IEnumerable<object[]> Opcode9AJsonFeeder()
    {
        yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "9a 28 e4", "initial": { "pc": 41217, "s": 7, "a": 109, "x": 185, "y": 102, "p": 168, "ram": [ [41217, 154], [41218, 40], [41219, 228]]}, "final": { "pc": 41218, "s": 185, "a": 109, "x": 185, "y": 102, "p": 168, "ram": [ [41217, 154], [41218, 40], [41219, 228]]}, "cycles": [ [41217, 154, "read"], [41218, 40, "read"]] }""") };
        yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "9a 27 d7", "initial": { "pc": 5395, "s": 249, "a": 100, "x": 89, "y": 185, "p": 224, "ram": [ [5395, 154], [5396, 39], [5397, 215]]}, "final": { "pc": 5396, "s": 89, "a": 100, "x": 89, "y": 185, "p": 224, "ram": [ [5395, 154], [5396, 39], [5397, 215]]}, "cycles": [ [5395, 154, "read"], [5396, 39, "read"]] }""") }; // negative flag not needed
        //yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "9a 8f 66", "initial": { "pc": 61438, "s": 41, "a": 42, "x": 2, "y": 170, "p": 34, "ram": [ [61438, 154], [61439, 143], [61440, 102]]}, "final": { "pc": 61439, "s": 2, "a": 42, "x": 2, "y": 170, "p": 34, "ram": [ [61438, 154], [61439, 143], [61440, 102]]}, "cycles": [ [61438, 154, "read"], [61439, 143, "read"]] }""") }; // zero flag not needed
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