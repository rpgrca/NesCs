using NesCs.Common.Tests;

namespace NesCs.UnitTests.Cpu;

public class TransferOpcodesMust
{
	[Theory]
    [MemberData(nameof(Opcode81JsonFeeder))]
    [MemberData(nameof(Opcode85JsonFeeder))]
    [MemberData(nameof(Opcode8AJsonFeeder))]
    [MemberData(nameof(Opcode8DJsonFeeder))]
    [MemberData(nameof(Opcode91JsonFeeder))]
    [MemberData(nameof(Opcode9AJsonFeeder))]
    [MemberData(nameof(Opcode95JsonFeeder))]
    [MemberData(nameof(Opcode98JsonFeeder))]
    [MemberData(nameof(Opcode99JsonFeeder))]
    [MemberData(nameof(Opcode9DJsonFeeder))]
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

    public static IEnumerable<object[]> Opcode81JsonFeeder()
    {
        /*   0  */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "81 38 b6", "initial": { "pc": 11515, "s": 110, "a": 125, "x": 216, "y": 5, "p": 232, "ram": [ [11515, 129], [11516, 56], [11517, 182], [56, 142], [16, 45], [17, 118]]}, "final": { "pc": 11517, "s": 110, "a": 125, "x": 216, "y": 5, "p": 232, "ram": [ [16, 45], [17, 118], [56, 142], [11515, 129], [11516, 56], [11517, 182], [30253, 125]]}, "cycles": [ [11515, 129, "read"], [11516, 56, "read"], [56, 142, "read"], [16, 45, "read"], [17, 118, "read"], [30253, 125, "write"]] }""") };
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

    public static IEnumerable<object[]> Opcode91JsonFeeder()
    {
        /* normal test   */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "91 12 c4", "initial": { "pc": 18971, "s": 177, "a": 50, "x": 202, "y": 94, "p": 225, "ram": [ [18971, 145], [18972, 18], [18973, 196], [18, 29], [19, 160], [41083, 155]]}, "final": { "pc": 18973, "s": 177, "a": 50, "x": 202, "y": 94, "p": 225, "ram": [ [18, 29], [19, 160], [18971, 145], [18972, 18], [18973, 196], [41083, 50]]}, "cycles": [ [18971, 145, "read"], [18972, 18, "read"], [18, 29, "read"], [19, 160, "read"], [41083, 155, "read"], [41083, 50, "write"]] }""") };
        /* boundary wrap */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "91 a5 c1", "initial": { "pc": 13446, "s": 224, "a": 91, "x": 209, "y": 153, "p": 163, "ram": [ [13446, 145], [13447, 165], [13448, 193], [165, 172], [166, 249], [63813, 147]]}, "final": { "pc": 13448, "s": 224, "a": 91, "x": 209, "y": 153, "p": 163, "ram": [ [165, 172], [166, 249], [13446, 145], [13447, 165], [13448, 193], [63813, 147], [64069, 91]]}, "cycles": [ [13446, 145, "read"], [13447, 165, "read"], [165, 172, "read"], [166, 249, "read"], [63813, 147, "read"], [64069, 91, "write"]] }""") };
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

    public static IEnumerable<object[]> Opcode99JsonFeeder()
    {
        /*   0  */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "99 f8 b7", "initial": { "pc": 45189, "s": 27, "a": 45, "x": 29, "y": 41, "p": 228, "ram": [ [45189, 153], [45190, 248], [45191, 183], [46881, 82], [45192, 231]]}, "final": { "pc": 45192, "s": 27, "a": 45, "x": 29, "y": 41, "p": 228, "ram": [ [45189, 153], [45190, 248], [45191, 183], [45192, 231], [46881, 82], [47137, 45]]}, "cycles": [ [45189, 153, "read"], [45190, 248, "read"], [45191, 183, "read"], [46881, 82, "read"], [47137, 45, "write"]] }""") };
    }

    public static IEnumerable<object[]> Opcode9DJsonFeeder()
    {
        /* normal test   */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "9d eb d2", "initial": { "pc": 36083, "s": 40, "a": 81, "x": 2, "y": 8, "p": 41, "ram": [ [36083, 157], [36084, 235], [36085, 210], [53997, 123], [36086, 174]]}, "final": { "pc": 36086, "s": 40, "a": 81, "x": 2, "y": 8, "p": 41, "ram": [ [36083, 157], [36084, 235], [36085, 210], [36086, 174], [53997, 81]]}, "cycles": [ [36083, 157, "read"], [36084, 235, "read"], [36085, 210, "read"], [53997, 123, "read"], [53997, 81, "write"]] }""") };
        /* boundary wrap */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "9d 94 e1", "initial": { "pc": 62551, "s": 41, "a": 31, "x": 127, "y": 7, "p": 168, "ram": [ [62551, 157], [62552, 148], [62553, 225], [57619, 79], [62554, 18]]}, "final": { "pc": 62554, "s": 41, "a": 31, "x": 127, "y": 7, "p": 168, "ram": [ [57619, 79], [57875, 31], [62551, 157], [62552, 148], [62553, 225], [62554, 18]]}, "cycles": [ [62551, 157, "read"], [62552, 148, "read"], [62553, 225, "read"], [57619, 79, "read"], [57875, 31, "write"]] }""") };
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