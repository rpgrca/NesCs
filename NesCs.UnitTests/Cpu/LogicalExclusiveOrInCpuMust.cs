using NesCs.Common.Tests;

namespace NesCs.UnitTests.Cpu;

public class LogicalExclusiveOrInCpuMust
{
	[Theory]
    [MemberData(nameof(Opcode45JsonFeeder))]
    [MemberData(nameof(Opcode49JsonFeeder))]
    [MemberData(nameof(Opcode4DJsonFeeder))]
    [MemberData(nameof(Opcode55JsonFeeder))]
    [MemberData(nameof(Opcode59JsonFeeder))]
    [MemberData(nameof(Opcode5DJsonFeeder))]
	public void BeExecutedCorrectly(SampleCpu sampleCpu)
	{
        var trace = new List<(int, byte, string)>();
        var sut = Utilities.CreateSubjectUnderTestFromSample(sampleCpu.Opcodes, sampleCpu.Initial, trace);
        sut.Run();

        Utilities.Equal(sampleCpu.Final, sut);
        Utilities.Equal(sampleCpu.Cycles, trace);
	}

    public static IEnumerable<object[]> Opcode45JsonFeeder()
    {
        /*   0    */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "45 2d 3a", "initial": { "pc": 36318, "s": 120, "a": 105, "x": 254, "y": 18, "p": 33, "ram": [ [36318, 69], [36319, 45], [36320, 58], [45, 21]]}, "final": { "pc": 36320, "s": 120, "a": 124, "x": 254, "y": 18, "p": 33, "ram": [ [45, 21], [36318, 69], [36319, 45], [36320, 58]]}, "cycles": [ [36318, 69, "read"], [36319, 45, "read"], [45, 21, "read"]] }""") };
        /*   2 Z  */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "45 67 40", "initial": { "pc": 10834, "s": 44, "a": 6, "x": 19, "y": 161, "p": 99, "ram": [ [10834, 69], [10835, 103], [10836, 64], [103, 92]]}, "final": { "pc": 10836, "s": 44, "a": 90, "x": 19, "y": 161, "p": 97, "ram": [ [103, 92], [10834, 69], [10835, 103], [10836, 64]]}, "cycles": [ [10834, 69, "read"], [10835, 103, "read"], [103, 92, "read"]] }""") };
        /* 128 N  */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "45 42 70", "initial": { "pc": 9462, "s": 233, "a": 252, "x": 61, "y": 182, "p": 44, "ram": [ [9462, 69], [9463, 66], [9464, 112], [66, 20]]}, "final": { "pc": 9464, "s": 233, "a": 232, "x": 61, "y": 182, "p": 172, "ram": [ [66, 20], [9462, 69], [9463, 66], [9464, 112]]}, "cycles": [ [9462, 69, "read"], [9463, 66, "read"], [66, 20, "read"]] }""") };
        /* 130 NZ */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "45 a6 26", "initial": { "pc": 50271, "s": 152, "a": 19, "x": 189, "y": 227, "p": 110, "ram": [ [50271, 69], [50272, 166], [50273, 38], [166, 205]]}, "final": { "pc": 50273, "s": 152, "a": 222, "x": 189, "y": 227, "p": 236, "ram": [ [166, 205], [50271, 69], [50272, 166], [50273, 38]]}, "cycles": [ [50271, 69, "read"], [50272, 166, "read"], [166, 205, "read"]] }""") };
    }

    public static IEnumerable<object[]> Opcode49JsonFeeder()
    {
        /*   0    */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "49 d9 22", "initial": { "pc": 53800, "s": 242, "a": 33, "x": 249, "y": 116, "p": 173, "ram": [ [53800, 73], [53801, 217], [53802, 34]]}, "final": { "pc": 53802, "s": 242, "a": 248, "x": 249, "y": 116, "p": 173, "ram": [ [53800, 73], [53801, 217], [53802, 34]]}, "cycles": [ [53800, 73, "read"], [53801, 217, "read"]] }""") };
        /*   2 Z  */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "49 b4 12", "initial": { "pc": 38951, "s": 46, "a": 110, "x": 186, "y": 118, "p": 174, "ram": [ [38951, 73], [38952, 180], [38953, 18]]}, "final": { "pc": 38953, "s": 46, "a": 218, "x": 186, "y": 118, "p": 172, "ram": [ [38951, 73], [38952, 180], [38953, 18]]}, "cycles": [ [38951, 73, "read"], [38952, 180, "read"]] }""") };
        /* 128 N  */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "49 78 c9", "initial": { "pc": 8256, "s": 18, "a": 65, "x": 41, "y": 182, "p": 168, "ram": [ [8256, 73], [8257, 120], [8258, 201]]}, "final": { "pc": 8258, "s": 18, "a": 57, "x": 41, "y": 182, "p": 40, "ram": [ [8256, 73], [8257, 120], [8258, 201]]}, "cycles": [ [8256, 73, "read"], [8257, 120, "read"]] }""") };
        /* 130 NZ */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "49 e1 2f", "initial": { "pc": 50746, "s": 129, "a": 218, "x": 97, "y": 117, "p": 171, "ram": [ [50746, 73], [50747, 225], [50748, 47]]}, "final": { "pc": 50748, "s": 129, "a": 59, "x": 97, "y": 117, "p": 41, "ram": [ [50746, 73], [50747, 225], [50748, 47]]}, "cycles": [ [50746, 73, "read"], [50747, 225, "read"]] }""") };
    }

    public static IEnumerable<object[]> Opcode4DJsonFeeder()
    {
        /*   0    */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "4d ed 1e", "initial": { "pc": 57462, "s": 249, "a": 71, "x": 234, "y": 59, "p": 37, "ram": [ [57462, 77], [57463, 237], [57464, 30], [7917, 121], [57465, 166]]}, "final": { "pc": 57465, "s": 249, "a": 62, "x": 234, "y": 59, "p": 37, "ram": [ [7917, 121], [57462, 77], [57463, 237], [57464, 30], [57465, 166]]}, "cycles": [ [57462, 77, "read"], [57463, 237, "read"], [57464, 30, "read"], [7917, 121, "read"]] }""") };
        /*   2 Z  */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "4d dc be", "initial": { "pc": 2403, "s": 160, "a": 89, "x": 145, "y": 141, "p": 98, "ram": [ [2403, 77], [2404, 220], [2405, 190], [48860, 9], [2406, 56]]}, "final": { "pc": 2406, "s": 160, "a": 80, "x": 145, "y": 141, "p": 96, "ram": [ [2403, 77], [2404, 220], [2405, 190], [2406, 56], [48860, 9]]}, "cycles": [ [2403, 77, "read"], [2404, 220, "read"], [2405, 190, "read"], [48860, 9, "read"]] }""") };
        /* 128 N  */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "4d 9c 85", "initial": { "pc": 49654, "s": 35, "a": 107, "x": 43, "y": 32, "p": 164, "ram": [ [49654, 77], [49655, 156], [49656, 133], [34204, 16], [49657, 204]]}, "final": { "pc": 49657, "s": 35, "a": 123, "x": 43, "y": 32, "p": 36, "ram": [ [34204, 16], [49654, 77], [49655, 156], [49656, 133], [49657, 204]]}, "cycles": [ [49654, 77, "read"], [49655, 156, "read"], [49656, 133, "read"], [34204, 16, "read"]] }""") };
        /* 130 NZ */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "4d 20 4b", "initial": { "pc": 16161, "s": 180, "a": 100, "x": 154, "y": 235, "p": 42, "ram": [ [16161, 77], [16162, 32], [16163, 75], [19232, 152], [16164, 209]]}, "final": { "pc": 16164, "s": 180, "a": 252, "x": 154, "y": 235, "p": 168, "ram": [ [16161, 77], [16162, 32], [16163, 75], [16164, 209], [19232, 152]]}, "cycles": [ [16161, 77, "read"], [16162, 32, "read"], [16163, 75, "read"], [19232, 152, "read"]] }""") };
    }

    public static IEnumerable<object[]> Opcode55JsonFeeder()
    {
        /*   0    */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "55 a0 77", "initial": { "pc": 42416, "s": 97, "a": 95, "x": 128, "y": 56, "p": 44, "ram": [ [42416, 85], [42417, 160], [42418, 119], [160, 219], [32, 89]]}, "final": { "pc": 42418, "s": 97, "a": 6, "x": 128, "y": 56, "p": 44, "ram": [ [32, 89], [160, 219], [42416, 85], [42417, 160], [42418, 119]]}, "cycles": [ [42416, 85, "read"], [42417, 160, "read"], [160, 219, "read"], [32, 89, "read"]] }""") };
        /*   2 Z  */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "55 e9 62", "initial": { "pc": 24494, "s": 96, "a": 65, "x": 230, "y": 169, "p": 163, "ram": [ [24494, 85], [24495, 233], [24496, 98], [233, 8], [207, 169]]}, "final": { "pc": 24496, "s": 96, "a": 232, "x": 230, "y": 169, "p": 161, "ram": [ [207, 169], [233, 8], [24494, 85], [24495, 233], [24496, 98]]}, "cycles": [ [24494, 85, "read"], [24495, 233, "read"], [233, 8, "read"], [207, 169, "read"]] }""") };
        /* 128 N  */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "55 33 dd", "initial": { "pc": 689, "s": 57, "a": 202, "x": 200, "y": 229, "p": 109, "ram": [ [689, 85], [690, 51], [691, 221], [51, 219], [251, 115]]}, "final": { "pc": 691, "s": 57, "a": 185, "x": 200, "y": 229, "p": 237, "ram": [ [51, 219], [251, 115], [689, 85], [690, 51], [691, 221]]}, "cycles": [ [689, 85, "read"], [690, 51, "read"], [51, 219, "read"], [251, 115, "read"]] }""") };
        /* 130 NZ */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "55 76 c0", "initial": { "pc": 40976, "s": 252, "a": 43, "x": 1, "y": 157, "p": 230, "ram": [ [40976, 85], [40977, 118], [40978, 192], [118, 210], [119, 60]]}, "final": { "pc": 40978, "s": 252, "a": 23, "x": 1, "y": 157, "p": 100, "ram": [ [118, 210], [119, 60], [40976, 85], [40977, 118], [40978, 192]]}, "cycles": [ [40976, 85, "read"], [40977, 118, "read"], [118, 210, "read"], [119, 60, "read"]] }""") };
    }

    public static IEnumerable<object[]> Opcode59JsonFeeder()
    {
        /*   0    */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "59 e8 d2", "initial": { "pc": 63202, "s": 35, "a": 56, "x": 41, "y": 88, "p": 237, "ram": [ [63202, 89], [63203, 232], [63204, 210], [53824, 135], [54080, 204], [63205, 4]]}, "final": { "pc": 63205, "s": 35, "a": 244, "x": 41, "y": 88, "p": 237, "ram": [ [53824, 135], [54080, 204], [63202, 89], [63203, 232], [63204, 210], [63205, 4]]}, "cycles": [ [63202, 89, "read"], [63203, 232, "read"], [63204, 210, "read"], [53824, 135, "read"], [54080, 204, "read"]] }""") };
        /*   2 Z  */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "59 4a a8", "initial": { "pc": 25408, "s": 199, "a": 2, "x": 25, "y": 192, "p": 103, "ram": [ [25408, 89], [25409, 74], [25410, 168], [43018, 73], [43274, 54], [25411, 76]]}, "final": { "pc": 25411, "s": 199, "a": 52, "x": 25, "y": 192, "p": 101, "ram": [ [25408, 89], [25409, 74], [25410, 168], [25411, 76], [43018, 73], [43274, 54]]}, "cycles": [ [25408, 89, "read"], [25409, 74, "read"], [25410, 168, "read"], [43018, 73, "read"], [43274, 54, "read"]] }""") };
        /* 128 N  */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "59 69 f6", "initial": { "pc": 65404, "s": 199, "a": 189, "x": 88, "y": 87, "p": 164, "ram": [ [65404, 89], [65405, 105], [65406, 246], [63168, 207], [65407, 146]]}, "final": { "pc": 65407, "s": 199, "a": 114, "x": 88, "y": 87, "p": 36, "ram": [ [63168, 207], [65404, 89], [65405, 105], [65406, 246], [65407, 146]]}, "cycles": [ [65404, 89, "read"], [65405, 105, "read"], [65406, 246, "read"], [63168, 207, "read"]] }""") };
        /* 130 NZ */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "59 b7 b7", "initial": { "pc": 32139, "s": 239, "a": 103, "x": 220, "y": 38, "p": 38, "ram": [ [32139, 89], [32140, 183], [32141, 183], [47069, 248], [32142, 192]]}, "final": { "pc": 32142, "s": 239, "a": 159, "x": 220, "y": 38, "p": 164, "ram": [ [32139, 89], [32140, 183], [32141, 183], [32142, 192], [47069, 248]]}, "cycles": [ [32139, 89, "read"], [32140, 183, "read"], [32141, 183, "read"], [47069, 248, "read"]] }""") };
    }

    public static IEnumerable<object[]> Opcode5DJsonFeeder()
    {
        /*   0    */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "5d 06 9e", "initial": { "pc": 57808, "s": 132, "a": 227, "x": 228, "y": 192, "p": 32, "ram": [ [57808, 93], [57809, 6], [57810, 158], [40682, 209], [57811, 70]]}, "final": { "pc": 57811, "s": 132, "a": 50, "x": 228, "y": 192, "p": 32, "ram": [ [40682, 209], [57808, 93], [57809, 6], [57810, 158], [57811, 70]]}, "cycles": [ [57808, 93, "read"], [57809, 6, "read"], [57810, 158, "read"], [40682, 209, "read"]] }""") };
        /*   2 Z  */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "5d d5 c8", "initial": { "pc": 29004, "s": 4, "a": 7, "x": 111, "y": 180, "p": 167, "ram": [ [29004, 93], [29005, 213], [29006, 200], [51268, 131], [51524, 222], [29007, 234]]}, "final": { "pc": 29007, "s": 4, "a": 217, "x": 111, "y": 180, "p": 165, "ram": [ [29004, 93], [29005, 213], [29006, 200], [29007, 234], [51268, 131], [51524, 222]]}, "cycles": [ [29004, 93, "read"], [29005, 213, "read"], [29006, 200, "read"], [51268, 131, "read"], [51524, 222, "read"]] }""") };
        /* 128 N  */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "5d 24 13", "initial": { "pc": 41811, "s": 106, "a": 58, "x": 182, "y": 226, "p": 225, "ram": [ [41811, 93], [41812, 36], [41813, 19], [5082, 46], [41814, 26]]}, "final": { "pc": 41814, "s": 106, "a": 20, "x": 182, "y": 226, "p": 97, "ram": [ [5082, 46], [41811, 93], [41812, 36], [41813, 19], [41814, 26]]}, "cycles": [ [41811, 93, "read"], [41812, 36, "read"], [41813, 19, "read"], [5082, 46, "read"]] }""") };
        /* 130 NZ */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "5d 12 86", "initial": { "pc": 9502, "s": 126, "a": 174, "x": 94, "y": 105, "p": 34, "ram": [ [9502, 93], [9503, 18], [9504, 134], [34416, 7], [9505, 114]]}, "final": { "pc": 9505, "s": 126, "a": 169, "x": 94, "y": 105, "p": 160, "ram": [ [9502, 93], [9503, 18], [9504, 134], [9505, 114], [34416, 7]]}, "cycles": [ [9502, 93, "read"], [9503, 18, "read"], [9504, 134, "read"], [34416, 7, "read"]] }""") };
    }
}