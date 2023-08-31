using NesCs.Common.Tests;

namespace NesCs.UnitTests.Cpu;

public class NopOperationsInCpuMust
{
    [Theory]
    [MemberData(nameof(Opcode04JsonFeeder))]
    [MemberData(nameof(Opcode0CJsonFeeder))]
    [MemberData(nameof(Opcode14JsonFeeder))]
    [MemberData(nameof(Opcode1AJsonFeeder))]
    [MemberData(nameof(Opcode1CJsonFeeder))]
    [MemberData(nameof(Opcode34JsonFeeder))]
    [MemberData(nameof(Opcode3AJsonFeeder))]
    [MemberData(nameof(Opcode3CJsonFeeder))]
    [MemberData(nameof(Opcode44JsonFeeder))]
    [MemberData(nameof(Opcode54JsonFeeder))]
    [MemberData(nameof(Opcode5AJsonFeeder))]
    [MemberData(nameof(Opcode5CJsonFeeder))]
    [MemberData(nameof(Opcode64JsonFeeder))]
    [MemberData(nameof(Opcode74JsonFeeder))]
    [MemberData(nameof(Opcode7AJsonFeeder))]
    [MemberData(nameof(Opcode7CJsonFeeder))]
    [MemberData(nameof(Opcode80JsonFeeder))]
    [MemberData(nameof(Opcode82JsonFeeder))]
    [MemberData(nameof(Opcode89JsonFeeder))]
    [MemberData(nameof(OpcodeC2JsonFeeder))]
    [MemberData(nameof(OpcodeD4JsonFeeder))]
    [MemberData(nameof(OpcodeDAJsonFeeder))]
    [MemberData(nameof(OpcodeDCJsonFeeder))]
    [MemberData(nameof(OpcodeE2JsonFeeder))]
    [MemberData(nameof(OpcodeEAJsonFeeder))]
    [MemberData(nameof(OpcodeF4JsonFeeder))]
    [MemberData(nameof(OpcodeFAJsonFeeder))]
    [MemberData(nameof(OpcodeFCJsonFeeder))]
    public void BeExecutedCorrectly(SampleCpu sampleCpu)
    {
        var trace = new List<(int, byte, string)>();
        var sut = Utilities.CreateSubjectUnderTestFromSample(sampleCpu.Initial, trace);
        sut.Step();

        Utilities.Equal(sampleCpu.Final, sut);
        Utilities.Equal(sampleCpu.Cycles, trace);
    }

    public static IEnumerable<object[]> Opcode04JsonFeeder()
    {
        /*   0  */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "04 f5 1e", "initial": { "pc": 17311, "s": 29, "a": 215, "x": 111, "y": 27, "p": 103, "ram": [ [17311, 4], [17312, 245], [17313, 30], [245, 59]]}, "final": { "pc": 17313, "s": 29, "a": 215, "x": 111, "y": 27, "p": 103, "ram": [ [245, 59], [17311, 4], [17312, 245], [17313, 30]]}, "cycles": [ [17311, 4, "read"], [17312, 245, "read"], [245, 59, "read"]] }""") };
    }

    public static IEnumerable<object[]> Opcode0CJsonFeeder()
    {
        /*   0  */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "0c 1", "initial": {"pc": 12398, "s": 10, "a": 42, "x": 74, "y": 146, "p": 179, "ram": [[18116, 131], [12400, 70], [12399, 196], [12398, 12]]}, "final": {"pc": 12401, "s": 10, "a": 42, "x": 74, "y": 146, "p": 179, "ram": [[18116, 131], [12400, 70], [12399, 196], [12398, 12]]}, "cycles": [[12398, 12, "read"],[12399, 196, "read"],[12400, 70, "read"],[18116, 131, "read"]]}""") };
    }

    public static IEnumerable<object[]> Opcode14JsonFeeder()
    {
        /*   0  */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "14 65 be", "initial": { "pc": 41348, "s": 207, "a": 139, "x": 9, "y": 204, "p": 38, "ram": [ [41348, 20], [41349, 101], [41350, 190], [101, 12], [110, 56]]}, "final": { "pc": 41350, "s": 207, "a": 139, "x": 9, "y": 204, "p": 38, "ram": [ [101, 12], [110, 56], [41348, 20], [41349, 101], [41350, 190]]}, "cycles": [ [41348, 20, "read"], [41349, 101, "read"], [101, 12, "read"], [110, 56, "read"]] }""") };
    }

    public static IEnumerable<object[]> Opcode1AJsonFeeder()
    {
        /*   0  */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "1a 66 12", "initial": { "pc": 52409, "s": 45, "a": 165, "x": 165, "y": 152, "p": 236, "ram": [ [52409, 26], [52410, 102], [52411, 18]]}, "final": { "pc": 52410, "s": 45, "a": 165, "x": 165, "y": 152, "p": 236, "ram": [ [52409, 26], [52410, 102], [52411, 18]]}, "cycles": [ [52409, 26, "read"], [52410, 102, "read"]] }""") };
    }

    public static IEnumerable<object[]> Opcode1CJsonFeeder()
    {
        /* common case            */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "1c 1", "initial": {"pc": 19430, "s": 182, "a": 69, "x": 23, "y": 85, "p": 50, "ram": [[59081, 119], [19432, 230], [19431, 178], [19430, 28]]}, "final": {"pc": 19433, "s": 182, "a": 69, "x": 23, "y": 85, "p": 50, "ram": [[59081, 119], [19432, 230], [19431, 178], [19430, 28]]}, "cycles": [[19430, 28, "read"],[19431, 178, "read"],[19432, 230, "read"],[59081, 119, "read"],[59081, 119, "read"]]}""") };
        /* second address bug     */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "1c 10000", "initial": {"pc": 21044, "s": 56, "a": 75, "x": 241, "y": 33, "p": 51, "ram": [[52402, 54], [52146, 220], [21046, 203], [21045, 193], [21044, 28]]}, "final": {"pc": 21047, "s": 56, "a": 75, "x": 241, "y": 33, "p": 51, "ram": [[52402, 54], [52146, 220], [21046, 203], [21045, 193], [21044, 28]]}, "cycles": [[21044, 28, "read"],[21045, 193, "read"],[21046, 203, "read"],[52146, 220, "read"],[52402, 54, "read"]]}""") };
        /* first address 0xff bug */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "1c 9999", "initial": {"pc": 29476, "s": 62, "a": 227, "x": 241, "y": 252, "p": 50, "ram": [[64322, 178], [64066, 116], [29478, 250], [29477, 81], [29476, 28]]}, "final": {"pc": 29479, "s": 62, "a": 227, "x": 241, "y": 252, "p": 50, "ram": [[64322, 178], [64066, 116], [29478, 250], [29477, 81], [29476, 28]]}, "cycles": [[29476, 28, "read"],[29477, 81, "read"],[29478, 250, "read"],[64066, 116, "read"],[64322, 178, "read"]]}""") };
        /* second add 0xffff bug  */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "1c 9934", "initial": {"pc": 5212, "s": 63, "a": 177, "x": 243, "y": 63, "p": 57, "ram": [[170, 24], [65450, 194], [5214, 255], [5213, 183], [5212, 28]]}, "final": {"pc": 5215, "s": 63, "a": 177, "x": 243, "y": 63, "p": 57, "ram": [[170, 24], [65450, 194], [5214, 255], [5213, 183], [5212, 28]]}, "cycles": [[5212, 28, "read"],[5213, 183, "read"],[5214, 255, "read"],[65450, 194, "read"],[170, 24, "read"]]}""") };
    }

    public static IEnumerable<object[]> Opcode34JsonFeeder()
    {
        /*   0  */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "34 94 6f", "initial": { "pc": 28616, "s": 11, "a": 204, "x": 31, "y": 62, "p": 167, "ram": [ [28616, 52], [28617, 148], [28618, 111], [148, 147], [179, 160]]}, "final": { "pc": 28618, "s": 11, "a": 204, "x": 31, "y": 62, "p": 167, "ram": [ [148, 147], [179, 160], [28616, 52], [28617, 148], [28618, 111]]}, "cycles": [ [28616, 52, "read"], [28617, 148, "read"], [148, 147, "read"], [179, 160, "read"]] }""") };
    }

    public static IEnumerable<object[]> Opcode3AJsonFeeder()
    {
        /*   0  */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "3a 86 c2", "initial": { "pc": 33018, "s": 138, "a": 6, "x": 93, "y": 192, "p": 110, "ram": [ [33018, 58], [33019, 134], [33020, 194]]}, "final": { "pc": 33019, "s": 138, "a": 6, "x": 93, "y": 192, "p": 110, "ram": [ [33018, 58], [33019, 134], [33020, 194]]}, "cycles": [ [33018, 58, "read"], [33019, 134, "read"]] }""") };
    }

    public static IEnumerable<object[]> Opcode3CJsonFeeder()
    {
        /* common case        */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "3c 1", "initial": {"pc": 33495, "s": 15, "a": 122, "x": 178, "y": 218, "p": 176, "ram": [[10047, 188], [33497, 38], [9791, 96], [33496, 141], [33495, 60]]}, "final": {"pc": 33498, "s": 15, "a": 122, "x": 178, "y": 218, "p": 176, "ram": [[10047, 188], [33497, 38], [9791, 96], [33496, 141], [33495, 60]]}, "cycles": [[33495, 60, "read"],[33496, 141, "read"],[33497, 38, "read"],[9791, 96, "read"],[10047, 188, "read"]]}""") };
        /* second address bug */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "3c 9992", "initial": {"pc": 62958, "s": 0, "a": 165, "x": 213, "y": 121, "p": 253, "ram": [[9305, 30], [9049, 5], [62960, 35], [62959, 132], [62958, 60]]}, "final": {"pc": 62961, "s": 0, "a": 165, "x": 213, "y": 121, "p": 253, "ram": [[9305, 30], [9049, 5], [62960, 35], [62959, 132], [62958, 60]]}, "cycles": [[62958, 60, "read"],[62959, 132, "read"],[62960, 35, "read"],[9049, 5, "read"],[9305, 30, "read"]]}""") };
        /* 0xffff wrap        */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "3c 9513", "initial": {"pc": 1506, "s": 165, "a": 201, "x": 165, "y": 61, "p": 112, "ram": [[24, 234], [65304, 203], [1508, 255], [1507, 115], [1506, 60]]}, "final": {"pc": 1509, "s": 165, "a": 201, "x": 165, "y": 61, "p": 112, "ram": [[24, 234], [65304, 203], [1508, 255], [1507, 115], [1506, 60]]}, "cycles": [[1506, 60, "read"],[1507, 115, "read"],[1508, 255, "read"],[65304, 203, "read"],[24, 234, "read"]]}""") };
    }

    public static IEnumerable<object[]> Opcode44JsonFeeder()
    {
        /*   0  */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "44 f6 d5", "initial": { "pc": 4885, "s": 99, "a": 141, "x": 54, "y": 238, "p": 32, "ram": [ [4885, 68], [4886, 246], [4887, 213], [246, 249]]}, "final": { "pc": 4887, "s": 99, "a": 141, "x": 54, "y": 238, "p": 32, "ram": [ [246, 249], [4885, 68], [4886, 246], [4887, 213]]}, "cycles": [ [4885, 68, "read"], [4886, 246, "read"], [246, 249, "read"]] }""") };
    }

    public static IEnumerable<object[]> Opcode54JsonFeeder()
    {
        /*   0  */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "54 69 95", "initial": { "pc": 8996, "s": 166, "a": 137, "x": 116, "y": 29, "p": 234, "ram": [ [8996, 84], [8997, 105], [8998, 149], [105, 100], [221, 19]]}, "final": { "pc": 8998, "s": 166, "a": 137, "x": 116, "y": 29, "p": 234, "ram": [ [105, 100], [221, 19], [8996, 84], [8997, 105], [8998, 149]]}, "cycles": [ [8996, 84, "read"], [8997, 105, "read"], [105, 100, "read"], [221, 19, "read"]] }""") };
    }

    public static IEnumerable<object[]> Opcode5AJsonFeeder()
    {
        /*   0  */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "5a 5d 4f", "initial": { "pc": 40939, "s": 57, "a": 196, "x": 210, "y": 77, "p": 100, "ram": [ [40939, 90], [40940, 93], [40941, 79]]}, "final": { "pc": 40940, "s": 57, "a": 196, "x": 210, "y": 77, "p": 100, "ram": [ [40939, 90], [40940, 93], [40941, 79]]}, "cycles": [ [40939, 90, "read"], [40940, 93, "read"]] }""") };
    }

    public static IEnumerable<object[]> Opcode5CJsonFeeder()
    {
        /*   0  */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "5c 1", "initial": {"pc": 47559, "s": 104, "a": 174, "x": 78, "y": 95, "p": 254, "ram": [[26038, 72], [47561, 101], [47560, 104], [47559, 92]]}, "final": {"pc": 47562, "s": 104, "a": 174, "x": 78, "y": 95, "p": 254, "ram": [[26038, 72], [47561, 101], [47560, 104], [47559, 92]]}, "cycles": [[47559, 92, "read"],[47560, 104, "read"],[47561, 101, "read"],[26038, 72, "read"],[26038, 72, "read"]]}""") };
    }

    public static IEnumerable<object[]> Opcode64JsonFeeder()
    {
        /*   0  */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "64 3a 84", "initial": { "pc": 53938, "s": 13, "a": 11, "x": 187, "y": 32, "p": 229, "ram": [ [53938, 100], [53939, 58], [53940, 132], [58, 185]]}, "final": { "pc": 53940, "s": 13, "a": 11, "x": 187, "y": 32, "p": 229, "ram": [ [58, 185], [53938, 100], [53939, 58], [53940, 132]]}, "cycles": [ [53938, 100, "read"], [53939, 58, "read"], [58, 185, "read"]] }""") };
    }

    public static IEnumerable<object[]> Opcode74JsonFeeder()
    {
        /*   0  */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "74 57 22", "initial": { "pc": 65151, "s": 66, "a": 89, "x": 115, "y": 240, "p": 234, "ram": [ [65151, 116], [65152, 87], [65153, 34], [87, 241], [202, 44]]}, "final": { "pc": 65153, "s": 66, "a": 89, "x": 115, "y": 240, "p": 234, "ram": [ [87, 241], [202, 44], [65151, 116], [65152, 87], [65153, 34]]}, "cycles": [ [65151, 116, "read"], [65152, 87, "read"], [87, 241, "read"], [202, 44, "read"]] }""") };
    }

   public static IEnumerable<object[]> Opcode7AJsonFeeder()
    {
        /*   0  */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "7a cf ed", "initial": { "pc": 43335, "s": 7, "a": 17, "x": 170, "y": 223, "p": 234, "ram": [ [43335, 122], [43336, 207], [43337, 237]]}, "final": { "pc": 43336, "s": 7, "a": 17, "x": 170, "y": 223, "p": 234, "ram": [ [43335, 122], [43336, 207], [43337, 237]]}, "cycles": [ [43335, 122, "read"], [43336, 207, "read"]] }""") };
    }

    public static IEnumerable<object[]> Opcode7CJsonFeeder()
    {
        /*   0  */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "7c 1", "initial": {"pc": 61623, "s": 193, "a": 227, "x": 233, "y": 229, "p": 124, "ram": [[42284, 49], [42540, 207], [61625, 165], [61624, 67], [61623, 124]]}, "final": {"pc": 61626, "s": 193, "a": 227, "x": 233, "y": 229, "p": 124, "ram": [[42284, 49], [42540, 207], [61625, 165], [61624, 67], [61623, 124]]}, "cycles": [[61623, 124, "read"],[61624, 67, "read"],[61625, 165, "read"],[42284, 49, "read"],[42540, 207, "read"]]}""") };
    }

    public static IEnumerable<object[]> Opcode80JsonFeeder()
    {
        /*   0  */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "80 b1 b7", "initial": { "pc": 30423, "s": 145, "a": 16, "x": 184, "y": 201, "p": 39, "ram": [ [30423, 128], [30424, 177], [30425, 183]]}, "final": { "pc": 30425, "s": 145, "a": 16, "x": 184, "y": 201, "p": 39, "ram": [ [30423, 128], [30424, 177], [30425, 183]]}, "cycles": [ [30423, 128, "read"], [30424, 177, "read"]] }""") };
    }

    public static IEnumerable<object[]> Opcode82JsonFeeder()
    {
        /*   0  */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "82 8c e5", "initial": { "pc": 38475, "s": 168, "a": 182, "x": 193, "y": 42, "p": 165, "ram": [ [38475, 130], [38476, 140], [38477, 229]]}, "final": { "pc": 38477, "s": 168, "a": 182, "x": 193, "y": 42, "p": 165, "ram": [ [38475, 130], [38476, 140], [38477, 229]]}, "cycles": [ [38475, 130, "read"], [38476, 140, "read"]] }""") };
    }

    public static IEnumerable<object[]> Opcode89JsonFeeder()
    {
        /*   0  */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "89 42 67", "initial": { "pc": 35522, "s": 205, "a": 194, "x": 193, "y": 7, "p": 225, "ram": [ [35522, 137], [35523, 66], [35524, 103]]}, "final": { "pc": 35524, "s": 205, "a": 194, "x": 193, "y": 7, "p": 225, "ram": [ [35522, 137], [35523, 66], [35524, 103]]}, "cycles": [ [35522, 137, "read"], [35523, 66, "read"]] }""") };
    }

    public static IEnumerable<object[]> OpcodeC2JsonFeeder()
    {
        /*   0  */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "c2 4f 20", "initial": { "pc": 20017, "s": 232, "a": 167, "x": 191, "y": 228, "p": 98, "ram": [ [20017, 194], [20018, 79], [20019, 32]]}, "final": { "pc": 20019, "s": 232, "a": 167, "x": 191, "y": 228, "p": 98, "ram": [ [20017, 194], [20018, 79], [20019, 32]]}, "cycles": [ [20017, 194, "read"], [20018, 79, "read"]] }""") };
    }

    public static IEnumerable<object[]> OpcodeD4JsonFeeder()
    {
        /*   0  */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "d4 b8 5d", "initial": { "pc": 56213, "s": 3, "a": 160, "x": 189, "y": 128, "p": 168, "ram": [ [56213, 212], [56214, 184], [56215, 93], [184, 4], [117, 217]]}, "final": { "pc": 56215, "s": 3, "a": 160, "x": 189, "y": 128, "p": 168, "ram": [ [117, 217], [184, 4], [56213, 212], [56214, 184], [56215, 93]]}, "cycles": [ [56213, 212, "read"], [56214, 184, "read"], [184, 4, "read"], [117, 217, "read"]] }""") };
    }

    public static IEnumerable<object[]> OpcodeDAJsonFeeder()
    {
        /*   0  */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "da 2b df", "initial": { "pc": 47980, "s": 83, "a": 175, "x": 213, "y": 94, "p": 39, "ram": [ [47980, 218], [47981, 43], [47982, 223]]}, "final": { "pc": 47981, "s": 83, "a": 175, "x": 213, "y": 94, "p": 39, "ram": [ [47980, 218], [47981, 43], [47982, 223]]}, "cycles": [ [47980, 218, "read"], [47981, 43, "read"]] }""") };
    }

    public static IEnumerable<object[]> OpcodeDCJsonFeeder()
    {
        /*   0                   */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "dc 1", "initial": {"pc": 38280, "s": 205, "a": 130, "x": 186, "y": 117, "p": 119, "ram": [[26254, 109], [25998, 234], [38282, 101], [38281, 212], [38280, 220]]}, "final": {"pc": 38283, "s": 205, "a": 130, "x": 186, "y": 117, "p": 119, "ram": [[26254, 109], [25998, 234], [38282, 101], [38281, 212], [38280, 220]]}, "cycles": [[38280, 220, "read"],[38281, 212, "read"],[38282, 101, "read"],[25998, 234, "read"],[26254, 109, "read"]]}""") };
        /* second address 0xffff */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "dc 9817", "initial": {"pc": 24737, "s": 83, "a": 150, "x": 87, "y": 175, "p": 120, "ram": [[71, 156], [65351, 193], [24739, 255], [24738, 240], [24737, 220]]}, "final": {"pc": 24740, "s": 83, "a": 150, "x": 87, "y": 175, "p": 120, "ram": [[71, 156], [65351, 193], [24739, 255], [24738, 240], [24737, 220]]}, "cycles": [[24737, 220, "read"],[24738, 240, "read"],[24739, 255, "read"],[65351, 193, "read"],[71, 156, "read"]]}""") };
    }

    public static IEnumerable<object[]> OpcodeE2JsonFeeder()
    {
        /*   0  */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "e2 8e 73", "initial": { "pc": 3264, "s": 50, "a": 4, "x": 109, "y": 164, "p": 42, "ram": [ [3264, 226], [3265, 142], [3266, 115]]}, "final": { "pc": 3266, "s": 50, "a": 4, "x": 109, "y": 164, "p": 42, "ram": [ [3264, 226], [3265, 142], [3266, 115]]}, "cycles": [ [3264, 226, "read"], [3265, 142, "read"]] }""") };
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

   public static IEnumerable<object[]> OpcodeFAJsonFeeder()
    {
        /*   0  */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "fa 9c 30", "initial": { "pc": 22969, "s": 135, "a": 201, "x": 129, "y": 99, "p": 42, "ram": [ [22969, 250], [22970, 156], [22971, 48]]}, "final": { "pc": 22970, "s": 135, "a": 201, "x": 129, "y": 99, "p": 42, "ram": [ [22969, 250], [22970, 156], [22971, 48]]}, "cycles": [ [22969, 250, "read"], [22970, 156, "read"]] }""") };
    }

    public static IEnumerable<object[]> OpcodeFCJsonFeeder()
    {
        /*   0       */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "fc 1", "initial": {"pc": 52345, "s": 38, "a": 183, "x": 86, "y": 250, "p": 245, "ram": [[42245, 210], [52347, 165], [42501, 246], [52346, 175], [52345, 252]]}, "final": {"pc": 52348, "s": 38, "a": 183, "x": 86, "y": 250, "p": 245, "ram": [[42245, 210], [52347, 165], [42501, 246], [52346, 175], [52345, 252]]}, "cycles": [[52345, 252, "read"],[52346, 175, "read"],[52347, 165, "read"],[42245, 210, "read"],[42501, 246, "read"]]}""") };
        /* page jump */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "fc 9380", "initial": {"pc": 9519, "s": 236, "a": 160, "x": 145, "y": 204, "p": 115, "ram": [[97, 213], [9521, 255], [9520, 208], [65377, 252], [9519, 252]]}, "final": {"pc": 9522, "s": 236, "a": 160, "x": 145, "y": 204, "p": 115, "ram": [[97, 213], [9521, 255], [9520, 208], [65377, 252], [9519, 252]]}, "cycles": [[9519, 252, "read"],[9520, 208, "read"],[9521, 255, "read"],[65377, 252, "read"],[97, 213, "read"]]}""") };
    }
}