using NesCs.Common.Tests;

namespace NesCs.UnitTests.Cpu;

public class TransferOpcodesMust
{
	[Theory]
    [MemberData(nameof(Opcode81JsonFeeder))]
    [MemberData(nameof(Opcode84JsonFeeder))]
    [MemberData(nameof(Opcode85JsonFeeder))]
    [MemberData(nameof(Opcode86JsonFeeder))]
    [MemberData(nameof(Opcode8AJsonFeeder))]
    [MemberData(nameof(Opcode8CJsonFeeder))]
    [MemberData(nameof(Opcode8DJsonFeeder))]
    [MemberData(nameof(Opcode8EJsonFeeder))]
    [MemberData(nameof(Opcode91JsonFeeder))]
    [MemberData(nameof(Opcode94JsonFeeder))]
    [MemberData(nameof(Opcode95JsonFeeder))]
    [MemberData(nameof(Opcode96JsonFeeder))]
    [MemberData(nameof(Opcode98JsonFeeder))]
    [MemberData(nameof(Opcode99JsonFeeder))]
    [MemberData(nameof(Opcode9AJsonFeeder))]
    [MemberData(nameof(Opcode9DJsonFeeder))]
    [MemberData(nameof(OpcodeA8JsonFeeder))]
    [MemberData(nameof(OpcodeAAJsonFeeder))]
    [MemberData(nameof(OpcodeBAJsonFeeder))]
	public void BeExecutedCorrectly(SampleCpu sampleCpu)
    {
        var trace = new List<(int, byte, string)>();
        var sut = Utilities.CreateSubjectUnderTestFromSample(sampleCpu.Initial, trace);
        sut.Run();

        Utilities.Equal(sampleCpu.Final, sut);
        Utilities.Equal(sampleCpu.Cycles, trace);
    }

    public static IEnumerable<object[]> Opcode81JsonFeeder()
    {
        /*   0  */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "81 38 b6", "initial": { "pc": 11515, "s": 110, "a": 125, "x": 216, "y": 5, "p": 232, "ram": [ [11515, 129], [11516, 56], [11517, 182], [56, 142], [16, 45], [17, 118]]}, "final": { "pc": 11517, "s": 110, "a": 125, "x": 216, "y": 5, "p": 232, "ram": [ [16, 45], [17, 118], [56, 142], [11515, 129], [11516, 56], [11517, 182], [30253, 125]]}, "cycles": [ [11515, 129, "read"], [11516, 56, "read"], [56, 142, "read"], [16, 45, "read"], [17, 118, "read"], [30253, 125, "write"]] }""") };
    }

    public static IEnumerable<object[]> Opcode84JsonFeeder()
    {
        /*   0  */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "84 59 c7", "initial": { "pc": 21725, "s": 42, "a": 155, "x": 186, "y": 55, "p": 98, "ram": [ [21725, 132], [21726, 89], [21727, 199]]}, "final": { "pc": 21727, "s": 42, "a": 155, "x": 186, "y": 55, "p": 98, "ram": [ [89, 55], [21725, 132], [21726, 89], [21727, 199]]}, "cycles": [ [21725, 132, "read"], [21726, 89, "read"], [89, 55, "write"]] }""") };
    }

    public static IEnumerable<object[]> Opcode85JsonFeeder()
    {
        /*   0  */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "85 c2 e1", "initial": { "pc": 5715, "s": 250, "a": 58, "x": 0, "y": 100, "p": 33, "ram": [ [5715, 133], [5716, 194], [5717, 225]]}, "final": { "pc": 5717, "s": 250, "a": 58, "x": 0, "y": 100, "p": 33, "ram": [ [194, 58], [5715, 133], [5716, 194], [5717, 225]]}, "cycles": [ [5715, 133, "read"], [5716, 194, "read"], [194, 58, "write"]] }""") };
    }

    public static IEnumerable<object[]> Opcode86JsonFeeder()
    {
        /*   0  */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "86 69 18", "initial": { "pc": 45098, "s": 23, "a": 25, "x": 149, "y": 47, "p": 104, "ram": [ [45098, 134], [45099, 105], [45100, 24]]}, "final": { "pc": 45100, "s": 23, "a": 25, "x": 149, "y": 47, "p": 104, "ram": [ [105, 149], [45098, 134], [45099, 105], [45100, 24]]}, "cycles": [ [45098, 134, "read"], [45099, 105, "read"], [105, 149, "write"]] }""") };
    }

    public static IEnumerable<object[]> Opcode8AJsonFeeder()
    {
        yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "8a cb b2", "initial": { "pc": 4244, "s": 33, "a": 154, "x": 54, "y": 246, "p": 42, "ram": [ [4244, 138], [4245, 203], [4246, 178]]}, "final": { "pc": 4245, "s": 33, "a": 54, "x": 54, "y": 246, "p": 40, "ram": [ [4244, 138], [4245, 203], [4246, 178]]}, "cycles": [ [4244, 138, "read"], [4245, 203, "read"]] }""") };
    }

    public static IEnumerable<object[]> Opcode8CJsonFeeder()
    {
        /*   0  */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "8c 3f c2", "initial": { "pc": 18479, "s": 75, "a": 53, "x": 220, "y": 76, "p": 171, "ram": [ [18479, 140], [18480, 63], [18481, 194], [18482, 49]]}, "final": { "pc": 18482, "s": 75, "a": 53, "x": 220, "y": 76, "p": 171, "ram": [ [18479, 140], [18480, 63], [18481, 194], [18482, 49], [49727, 76]]}, "cycles": [ [18479, 140, "read"], [18480, 63, "read"], [18481, 194, "read"], [49727, 76, "write"]] }""") };
    }

    public static IEnumerable<object[]> Opcode8DJsonFeeder()
    {
        /*   0  */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "8d aa 66", "initial": { "pc": 49817, "s": 119, "a": 16, "x": 102, "y": 155, "p": 232, "ram": [ [49817, 141], [49818, 170], [49819, 102], [49820, 213]]}, "final": { "pc": 49820, "s": 119, "a": 16, "x": 102, "y": 155, "p": 232, "ram": [ [26282, 16], [49817, 141], [49818, 170], [49819, 102], [49820, 213]]}, "cycles": [ [49817, 141, "read"], [49818, 170, "read"], [49819, 102, "read"], [26282, 16, "write"]] }""") };
    }

    public static IEnumerable<object[]> Opcode8EJsonFeeder()
    {
        /*   0  */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "8e 28 6c", "initial": { "pc": 5602, "s": 240, "a": 22, "x": 221, "y": 163, "p": 173, "ram": [ [5602, 142], [5603, 40], [5604, 108], [5605, 77]]}, "final": { "pc": 5605, "s": 240, "a": 22, "x": 221, "y": 163, "p": 173, "ram": [ [5602, 142], [5603, 40], [5604, 108], [5605, 77], [27688, 221]]}, "cycles": [ [5602, 142, "read"], [5603, 40, "read"], [5604, 108, "read"], [27688, 221, "write"]] }""") };
    }

    public static IEnumerable<object[]> Opcode91JsonFeeder()
    {
        /* normal test   */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "91 12 c4", "initial": { "pc": 18971, "s": 177, "a": 50, "x": 202, "y": 94, "p": 225, "ram": [ [18971, 145], [18972, 18], [18973, 196], [18, 29], [19, 160], [41083, 155]]}, "final": { "pc": 18973, "s": 177, "a": 50, "x": 202, "y": 94, "p": 225, "ram": [ [18, 29], [19, 160], [18971, 145], [18972, 18], [18973, 196], [41083, 50]]}, "cycles": [ [18971, 145, "read"], [18972, 18, "read"], [18, 29, "read"], [19, 160, "read"], [41083, 155, "read"], [41083, 50, "write"]] }""") };
        /* boundary wrap */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "91 a5 c1", "initial": { "pc": 13446, "s": 224, "a": 91, "x": 209, "y": 153, "p": 163, "ram": [ [13446, 145], [13447, 165], [13448, 193], [165, 172], [166, 249], [63813, 147]]}, "final": { "pc": 13448, "s": 224, "a": 91, "x": 209, "y": 153, "p": 163, "ram": [ [165, 172], [166, 249], [13446, 145], [13447, 165], [13448, 193], [63813, 147], [64069, 91]]}, "cycles": [ [13446, 145, "read"], [13447, 165, "read"], [165, 172, "read"], [166, 249, "read"], [63813, 147, "read"], [64069, 91, "write"]] }""") };
    }

    public static IEnumerable<object[]> Opcode94JsonFeeder()
    {
        /*   0  */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "94 23 8e", "initial": { "pc": 27400, "s": 14, "a": 48, "x": 109, "y": 204, "p": 38, "ram": [ [27400, 148], [27401, 35], [27402, 142], [35, 173]]}, "final": { "pc": 27402, "s": 14, "a": 48, "x": 109, "y": 204, "p": 38, "ram": [ [35, 173], [144, 204], [27400, 148], [27401, 35], [27402, 142]]}, "cycles": [ [27400, 148, "read"], [27401, 35, "read"], [35, 173, "read"], [144, 204, "write"]] }""") };
    }

    public static IEnumerable<object[]> Opcode95JsonFeeder()
    {
        /*   0  */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "95 dd 50", "initial": { "pc": 34346, "s": 195, "a": 118, "x": 249, "y": 148, "p": 34, "ram": [ [34346, 149], [34347, 221], [34348, 80], [221, 25]]}, "final": { "pc": 34348, "s": 195, "a": 118, "x": 249, "y": 148, "p": 34, "ram": [ [214, 118], [221, 25], [34346, 149], [34347, 221], [34348, 80]]}, "cycles": [ [34346, 149, "read"], [34347, 221, "read"], [221, 25, "read"], [214, 118, "write"]] }""") };
    }

    public static IEnumerable<object[]> Opcode96JsonFeeder()
    {
        /*   0  */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "96 59 4f", "initial": { "pc": 40932, "s": 175, "a": 104, "x": 225, "y": 122, "p": 228, "ram": [ [40932, 150], [40933, 89], [40934, 79], [89, 220]]}, "final": { "pc": 40934, "s": 175, "a": 104, "x": 225, "y": 122, "p": 228, "ram": [ [89, 220], [211, 225], [40932, 150], [40933, 89], [40934, 79]]}, "cycles": [ [40932, 150, "read"], [40933, 89, "read"], [89, 220, "read"], [211, 225, "write"]] }""") };
    }

    public static IEnumerable<object[]> Opcode98JsonFeeder()
    {
        yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "98 88 51", "initial": { "pc": 35923, "s": 254, "a": 69, "x": 136, "y": 44, "p": 166, "ram": [ [35923, 152], [35924, 136], [35925, 81]]}, "final": { "pc": 35924, "s": 254, "a": 44, "x": 136, "y": 44, "p": 36, "ram": [ [35923, 152], [35924, 136], [35925, 81]]}, "cycles": [ [35923, 152, "read"], [35924, 136, "read"]] }""") };
    }

    public static IEnumerable<object[]> Opcode99JsonFeeder()
    {
        /*   0  */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "99 f8 b7", "initial": { "pc": 45189, "s": 27, "a": 45, "x": 29, "y": 41, "p": 228, "ram": [ [45189, 153], [45190, 248], [45191, 183], [46881, 82], [45192, 231]]}, "final": { "pc": 45192, "s": 27, "a": 45, "x": 29, "y": 41, "p": 228, "ram": [ [45189, 153], [45190, 248], [45191, 183], [45192, 231], [46881, 82], [47137, 45]]}, "cycles": [ [45189, 153, "read"], [45190, 248, "read"], [45191, 183, "read"], [46881, 82, "read"], [47137, 45, "write"]] }""") };
    }

    public static IEnumerable<object[]> Opcode9AJsonFeeder()
    {
        yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "9a 28 e4", "initial": { "pc": 41217, "s": 7, "a": 109, "x": 185, "y": 102, "p": 168, "ram": [ [41217, 154], [41218, 40], [41219, 228]]}, "final": { "pc": 41218, "s": 185, "a": 109, "x": 185, "y": 102, "p": 168, "ram": [ [41217, 154], [41218, 40], [41219, 228]]}, "cycles": [ [41217, 154, "read"], [41218, 40, "read"]] }""") };
        yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "9a 27 d7", "initial": { "pc": 5395, "s": 249, "a": 100, "x": 89, "y": 185, "p": 224, "ram": [ [5395, 154], [5396, 39], [5397, 215]]}, "final": { "pc": 5396, "s": 89, "a": 100, "x": 89, "y": 185, "p": 224, "ram": [ [5395, 154], [5396, 39], [5397, 215]]}, "cycles": [ [5395, 154, "read"], [5396, 39, "read"]] }""") }; // negative flag not affected
        yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "9a 8f 66", "initial": { "pc": 61438, "s": 41, "a": 42, "x": 2, "y": 170, "p": 34, "ram": [ [61438, 154], [61439, 143], [61440, 102]]}, "final": { "pc": 61439, "s": 2, "a": 42, "x": 2, "y": 170, "p": 34, "ram": [ [61438, 154], [61439, 143], [61440, 102]]}, "cycles": [ [61438, 154, "read"], [61439, 143, "read"]] }""") }; // zero flag not affected
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

    [Theory]
    [MemberData(nameof(OpcodeA7JsonFeeder))]
    [MemberData(nameof(OpcodeAFJsonFeeder))]
    [MemberData(nameof(OpcodeB3JsonFeeder))]
    [MemberData(nameof(OpcodeB7JsonFeeder))]
    [MemberData(nameof(OpcodeBFJsonFeeder))]
	public void BeExecutedCorrectly_WhenTheyAreIllegalInstructions(SampleCpu sampleCpu)
    {
        var trace = new List<(int, byte, string)>();
        var sut = Utilities.CreateSubjectUnderTestFromSample(sampleCpu.Initial, trace);
        sut.Run();

        Utilities.Equal(sampleCpu.Final, sut);
        Utilities.Equal(sampleCpu.Cycles, trace);
    }

    public static IEnumerable<object[]> OpcodeA7JsonFeeder()
    {
        /*   0    */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "a7 e9 cd", "initial": { "pc": 20348, "s": 59, "a": 123, "x": 218, "y": 224, "p": 165, "ram": [ [20348, 167], [20349, 233], [20350, 205], [233, 240]]}, "final": { "pc": 20350, "s": 59, "a": 240, "x": 240, "y": 224, "p": 165, "ram": [ [233, 240], [20348, 167], [20349, 233], [20350, 205]]}, "cycles": [ [20348, 167, "read"], [20349, 233, "read"], [233, 240, "read"]] }""") };
        /*   2 Z  */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "a7 1d 86", "initial": { "pc": 15822, "s": 161, "a": 39, "x": 215, "y": 78, "p": 162, "ram": [ [15822, 167], [15823, 29], [15824, 134], [29, 149]]}, "final": { "pc": 15824, "s": 161, "a": 149, "x": 149, "y": 78, "p": 160, "ram": [ [29, 149], [15822, 167], [15823, 29], [15824, 134]]}, "cycles": [ [15822, 167, "read"], [15823, 29, "read"], [29, 149, "read"]] }""") };
        /* 128 N  */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "a7 45 35", "initial": { "pc": 49957, "s": 240, "a": 171, "x": 72, "y": 207, "p": 96, "ram": [ [49957, 167], [49958, 69], [49959, 53], [69, 206]]}, "final": { "pc": 49959, "s": 240, "a": 206, "x": 206, "y": 207, "p": 224, "ram": [ [69, 206], [49957, 167], [49958, 69], [49959, 53]]}, "cycles": [ [49957, 167, "read"], [49958, 69, "read"], [69, 206, "read"]] }""") };
        /* 130 NZ */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "a7 bb 7a", "initial": { "pc": 59503, "s": 181, "a": 48, "x": 211, "y": 249, "p": 162, "ram": [ [59503, 167], [59504, 187], [59505, 122], [187, 34]]}, "final": { "pc": 59505, "s": 181, "a": 34, "x": 34, "y": 249, "p": 32, "ram": [ [187, 34], [59503, 167], [59504, 187], [59505, 122]]}, "cycles": [ [59503, 167, "read"], [59504, 187, "read"], [187, 34, "read"]] }""") };
    }

    public static IEnumerable<object[]> OpcodeAFJsonFeeder()
    {
        /*   0    */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "af 7b 41", "initial": { "pc": 44374, "s": 25, "a": 200, "x": 25, "y": 24, "p": 104, "ram": [ [44374, 175], [44375, 123], [44376, 65], [16763, 83], [44377, 72]]}, "final": { "pc": 44377, "s": 25, "a": 83, "x": 83, "y": 24, "p": 104, "ram": [ [16763, 83], [44374, 175], [44375, 123], [44376, 65], [44377, 72]]}, "cycles": [ [44374, 175, "read"], [44375, 123, "read"], [44376, 65, "read"], [16763, 83, "read"]] }""") };
        /*   2 Z  */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "af eb 89", "initial": { "pc": 29951, "s": 125, "a": 216, "x": 49, "y": 185, "p": 238, "ram": [ [29951, 175], [29952, 235], [29953, 137], [35307, 201], [29954, 195]]}, "final": { "pc": 29954, "s": 125, "a": 201, "x": 201, "y": 185, "p": 236, "ram": [ [29951, 175], [29952, 235], [29953, 137], [29954, 195], [35307, 201]]}, "cycles": [ [29951, 175, "read"], [29952, 235, "read"], [29953, 137, "read"], [35307, 201, "read"]] }""") };
        /* 128 N  */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "af 0e 1a", "initial": { "pc": 4801, "s": 92, "a": 200, "x": 178, "y": 37, "p": 36, "ram": [ [4801, 175], [4802, 14], [4803, 26], [6670, 207], [4804, 222]]}, "final": { "pc": 4804, "s": 92, "a": 207, "x": 207, "y": 37, "p": 164, "ram": [ [4801, 175], [4802, 14], [4803, 26], [4804, 222], [6670, 207]]}, "cycles": [ [4801, 175, "read"], [4802, 14, "read"], [4803, 26, "read"], [6670, 207, "read"]] }""") };
        /* 130 NZ */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "af 56 6d", "initial": { "pc": 32953, "s": 74, "a": 231, "x": 159, "y": 76, "p": 231, "ram": [ [32953, 175], [32954, 86], [32955, 109], [27990, 1], [32956, 21]]}, "final": { "pc": 32956, "s": 74, "a": 1, "x": 1, "y": 76, "p": 101, "ram": [ [27990, 1], [32953, 175], [32954, 86], [32955, 109], [32956, 21]]}, "cycles": [ [32953, 175, "read"], [32954, 86, "read"], [32955, 109, "read"], [27990, 1, "read"]] }""") };
    }

    public static IEnumerable<object[]> OpcodeB3JsonFeeder()
    {
        /*   0    */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "b3 74 ba", "initial": { "pc": 38812, "s": 33, "a": 103, "x": 52, "y": 23, "p": 41, "ram": [ [38812, 179], [38813, 116], [38814, 186], [116, 140], [117, 94], [24227, 5]]}, "final": { "pc": 38814, "s": 33, "a": 5, "x": 5, "y": 23, "p": 41, "ram": [ [116, 140], [117, 94], [24227, 5], [38812, 179], [38813, 116], [38814, 186]]}, "cycles": [ [38812, 179, "read"], [38813, 116, "read"], [116, 140, "read"], [117, 94, "read"], [24227, 5, "read"]] }""") };
        /*   2 Z  */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "b3 04 1e", "initial": { "pc": 1734, "s": 175, "a": 224, "x": 7, "y": 248, "p": 171, "ram": [ [1734, 179], [1735, 4], [1736, 30], [4, 130], [5, 134], [34426, 141], [34682, 234]]}, "final": { "pc": 1736, "s": 175, "a": 234, "x": 234, "y": 248, "p": 169, "ram": [ [4, 130], [5, 134], [1734, 179], [1735, 4], [1736, 30], [34426, 141], [34682, 234]]}, "cycles": [ [1734, 179, "read"], [1735, 4, "read"], [4, 130, "read"], [5, 134, "read"], [34426, 141, "read"], [34682, 234, "read"]] }""") };
        /* 128 N  */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "b3 56 9e", "initial": { "pc": 20051, "s": 71, "a": 201, "x": 18, "y": 109, "p": 165, "ram": [ [20051, 179], [20052, 86], [20053, 158], [86, 185], [87, 63], [16166, 214], [16422, 93]]}, "final": { "pc": 20053, "s": 71, "a": 93, "x": 93, "y": 109, "p": 37, "ram": [ [86, 185], [87, 63], [16166, 214], [16422, 93], [20051, 179], [20052, 86], [20053, 158]]}, "cycles": [ [20051, 179, "read"], [20052, 86, "read"], [86, 185, "read"], [87, 63, "read"], [16166, 214, "read"], [16422, 93, "read"]] }""") };
        /* 130 NZ */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "b3 79 29", "initial": { "pc": 8883, "s": 11, "a": 234, "x": 255, "y": 113, "p": 227, "ram": [ [8883, 179], [8884, 121], [8885, 41], [121, 44], [122, 42], [10909, 127]]}, "final": { "pc": 8885, "s": 11, "a": 127, "x": 127, "y": 113, "p": 97, "ram": [ [121, 44], [122, 42], [8883, 179], [8884, 121], [8885, 41], [10909, 127]]}, "cycles": [ [8883, 179, "read"], [8884, 121, "read"], [121, 44, "read"], [122, 42, "read"], [10909, 127, "read"]] }""") };
    }

    public static IEnumerable<object[]> OpcodeB7JsonFeeder()
    {
        /*   0    */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "b7 0c cb", "initial": { "pc": 51556, "s": 58, "a": 209, "x": 51, "y": 61, "p": 96, "ram": [ [51556, 183], [51557, 12], [51558, 203], [12, 214], [73, 19]]}, "final": { "pc": 51558, "s": 58, "a": 19, "x": 19, "y": 61, "p": 96, "ram": [ [12, 214], [73, 19], [51556, 183], [51557, 12], [51558, 203]]}, "cycles": [ [51556, 183, "read"], [51557, 12, "read"], [12, 214, "read"], [73, 19, "read"]] }""") };
        /*   2 Z  */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "b7 05 3e", "initial": { "pc": 23383, "s": 120, "a": 39, "x": 137, "y": 78, "p": 174, "ram": [ [23383, 183], [23384, 5], [23385, 62], [5, 46], [83, 202]]}, "final": { "pc": 23385, "s": 120, "a": 202, "x": 202, "y": 78, "p": 172, "ram": [ [5, 46], [83, 202], [23383, 183], [23384, 5], [23385, 62]]}, "cycles": [ [23383, 183, "read"], [23384, 5, "read"], [5, 46, "read"], [83, 202, "read"]] }""") };
        /* 128 N  */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "b7 62 a2", "initial": { "pc": 5926, "s": 186, "a": 105, "x": 182, "y": 184, "p": 173, "ram": [ [5926, 183], [5927, 98], [5928, 162], [98, 106], [26, 11]]}, "final": { "pc": 5928, "s": 186, "a": 11, "x": 11, "y": 184, "p": 45, "ram": [ [26, 11], [98, 106], [5926, 183], [5927, 98], [5928, 162]]}, "cycles": [ [5926, 183, "read"], [5927, 98, "read"], [98, 106, "read"], [26, 11, "read"]] }""") };
        /* 130 NZ */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "b7 12 7d", "initial": { "pc": 46457, "s": 98, "a": 86, "x": 75, "y": 203, "p": 103, "ram": [ [46457, 183], [46458, 18], [46459, 125], [18, 144], [221, 143]]}, "final": { "pc": 46459, "s": 98, "a": 143, "x": 143, "y": 203, "p": 229, "ram": [ [18, 144], [221, 143], [46457, 183], [46458, 18], [46459, 125]]}, "cycles": [ [46457, 183, "read"], [46458, 18, "read"], [18, 144, "read"], [221, 143, "read"]] }""") };
    }

    public static IEnumerable<object[]> OpcodeBFJsonFeeder()
    {
        /*   0    */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "bf 2e f8", "initial": { "pc": 19955, "s": 115, "a": 168, "x": 210, "y": 133, "p": 44, "ram": [ [19955, 191], [19956, 46], [19957, 248], [63667, 53], [19958, 73]]}, "final": { "pc": 19958, "s": 115, "a": 53, "x": 53, "y": 133, "p": 44, "ram": [ [19955, 191], [19956, 46], [19957, 248], [19958, 73], [63667, 53]]}, "cycles": [ [19955, 191, "read"], [19956, 46, "read"], [19957, 248, "read"], [63667, 53, "read"]] }""") };
        /*   2 Z  */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "bf 4f 0e", "initial": { "pc": 53649, "s": 50, "a": 50, "x": 80, "y": 228, "p": 98, "ram": [ [53649, 191], [53650, 79], [53651, 14], [3635, 183], [3891, 110], [53652, 84]]}, "final": { "pc": 53652, "s": 50, "a": 110, "x": 110, "y": 228, "p": 96, "ram": [ [3635, 183], [3891, 110], [53649, 191], [53650, 79], [53651, 14], [53652, 84]]}, "cycles": [ [53649, 191, "read"], [53650, 79, "read"], [53651, 14, "read"], [3635, 183, "read"], [3891, 110, "read"]] }""") };
        /* 128 N  */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "bf 63 74", "initial": { "pc": 41526, "s": 11, "a": 70, "x": 53, "y": 255, "p": 164, "ram": [ [41526, 191], [41527, 99], [41528, 116], [29794, 251], [30050, 93], [41529, 17]]}, "final": { "pc": 41529, "s": 11, "a": 93, "x": 93, "y": 255, "p": 36, "ram": [ [29794, 251], [30050, 93], [41526, 191], [41527, 99], [41528, 116], [41529, 17]]}, "cycles": [ [41526, 191, "read"], [41527, 99, "read"], [41528, 116, "read"], [29794, 251, "read"], [30050, 93, "read"]] }""") };
        /* 130 NZ */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "bf 81 37", "initial": { "pc": 18413, "s": 195, "a": 2, "x": 71, "y": 112, "p": 34, "ram": [ [18413, 191], [18414, 129], [18415, 55], [14321, 215], [18416, 216]]}, "final": { "pc": 18416, "s": 195, "a": 215, "x": 215, "y": 112, "p": 160, "ram": [ [14321, 215], [18413, 191], [18414, 129], [18415, 55], [18416, 216]]}, "cycles": [ [18413, 191, "read"], [18414, 129, "read"], [18415, 55, "read"], [14321, 215, "read"]] }""") };
    }
}