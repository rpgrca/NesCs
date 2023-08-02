using NesCs.Common.Tests;

namespace NesCs.UnitTests.Cpu;

public class TransferOpcodesMust
{
	[Theory]
    [MemberData(nameof(Opcode85JsonFeeder))]
    [MemberData(nameof(Opcode8AJsonFeeder))]
    [MemberData(nameof(Opcode8DJsonFeeder))]
    [MemberData(nameof(Opcode9AJsonFeeder))]
    [MemberData(nameof(Opcode95JsonFeeder))]
    [MemberData(nameof(Opcode98JsonFeeder))]
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

    public static IEnumerable<object[]> Opcode85JsonFeeder()
    {
        /*   0  */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "85 c2 e1", "initial": { "pc": 5715, "s": 250, "a": 58, "x": 0, "y": 100, "p": 33, "ram": [ [5715, 133], [5716, 194], [5717, 225]]}, "final": { "pc": 5717, "s": 250, "a": 58, "x": 0, "y": 100, "p": 33, "ram": [ [194, 58], [5715, 133], [5716, 194], [5717, 225]]}, "cycles": [ [5715, 133, "read"], [5716, 194, "read"], [194, 58, "write"]] }""") };
    }

    public static IEnumerable<object[]> Opcode8AJsonFeeder()
    {
        yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "8a cb b2", "initial": { "pc": 4244, "s": 33, "a": 154, "x": 54, "y": 246, "p": 42, "ram": [ [4244, 138], [4245, 203], [4246, 178]]}, "final": { "pc": 4245, "s": 33, "a": 54, "x": 54, "y": 246, "p": 40, "ram": [ [4244, 138], [4245, 203], [4246, 178]]}, "cycles": [ [4244, 138, "read"], [4245, 203, "read"]] }""") };
    }

    public static IEnumerable<object[]> Opcode8DJsonFeeder()
    {
        /*   0  */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "8d aa 66", "initial": { "pc": 49817, "s": 119, "a": 16, "x": 102, "y": 155, "p": 232, "ram": [ [49817, 141], [49818, 170], [49819, 102], [49820, 213]]}, "final": { "pc": 49820, "s": 119, "a": 16, "x": 102, "y": 155, "p": 232, "ram": [ [26282, 16], [49817, 141], [49818, 170], [49819, 102], [49820, 213]]}, "cycles": [ [49817, 141, "read"], [49818, 170, "read"], [49819, 102, "read"], [26282, 16, "write"]] }""") };
    }

    public static IEnumerable<object[]> Opcode95JsonFeeder()
    {
        /*   0  */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "95 dd 50", "initial": { "pc": 34346, "s": 195, "a": 118, "x": 249, "y": 148, "p": 34, "ram": [ [34346, 149], [34347, 221], [34348, 80], [221, 25]]}, "final": { "pc": 34348, "s": 195, "a": 118, "x": 249, "y": 148, "p": 34, "ram": [ [214, 118], [221, 25], [34346, 149], [34347, 221], [34348, 80]]}, "cycles": [ [34346, 149, "read"], [34347, 221, "read"], [221, 25, "read"], [214, 118, "write"]] }""") };
    }

    public static IEnumerable<object[]> Opcode9AJsonFeeder()
    {
        yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "9a 28 e4", "initial": { "pc": 41217, "s": 7, "a": 109, "x": 185, "y": 102, "p": 168, "ram": [ [41217, 154], [41218, 40], [41219, 228]]}, "final": { "pc": 41218, "s": 185, "a": 109, "x": 185, "y": 102, "p": 168, "ram": [ [41217, 154], [41218, 40], [41219, 228]]}, "cycles": [ [41217, 154, "read"], [41218, 40, "read"]] }""") };
        yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "9a 27 d7", "initial": { "pc": 5395, "s": 249, "a": 100, "x": 89, "y": 185, "p": 224, "ram": [ [5395, 154], [5396, 39], [5397, 215]]}, "final": { "pc": 5396, "s": 89, "a": 100, "x": 89, "y": 185, "p": 224, "ram": [ [5395, 154], [5396, 39], [5397, 215]]}, "cycles": [ [5395, 154, "read"], [5396, 39, "read"]] }""") }; // negative flag not affected
        yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "9a 8f 66", "initial": { "pc": 61438, "s": 41, "a": 42, "x": 2, "y": 170, "p": 34, "ram": [ [61438, 154], [61439, 143], [61440, 102]]}, "final": { "pc": 61439, "s": 2, "a": 42, "x": 2, "y": 170, "p": 34, "ram": [ [61438, 154], [61439, 143], [61440, 102]]}, "cycles": [ [61438, 154, "read"], [61439, 143, "read"]] }""") }; // zero flag not affected
    }

    public static IEnumerable<object[]> Opcode98JsonFeeder()
    {
        yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "98 88 51", "initial": { "pc": 35923, "s": 254, "a": 69, "x": 136, "y": 44, "p": 166, "ram": [ [35923, 152], [35924, 136], [35925, 81]]}, "final": { "pc": 35924, "s": 254, "a": 44, "x": 136, "y": 44, "p": 36, "ram": [ [35923, 152], [35924, 136], [35925, 81]]}, "cycles": [ [35923, 152, "read"], [35924, 136, "read"]] }""") };
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