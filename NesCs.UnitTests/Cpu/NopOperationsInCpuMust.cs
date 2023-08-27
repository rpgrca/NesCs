using NesCs.Common.Tests;

namespace NesCs.UnitTests.Cpu;

public class NopOperationsInCpuMust
{
    [Theory]
    [MemberData(nameof(Opcode04JsonFeeder))]
    [MemberData(nameof(Opcode14JsonFeeder))]
    [MemberData(nameof(Opcode1AJsonFeeder))]
    [MemberData(nameof(Opcode1CJsonFeeder))]
    [MemberData(nameof(Opcode34JsonFeeder))]
    [MemberData(nameof(Opcode3AJsonFeeder))]
    [MemberData(nameof(Opcode3CJsonFeeder))]
    [MemberData(nameof(Opcode44JsonFeeder))]
    [MemberData(nameof(Opcode54JsonFeeder))]
    [MemberData(nameof(Opcode5AJsonFeeder))]
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
    [MemberData(nameof(OpcodeE2JsonFeeder))]
    [MemberData(nameof(OpcodeEAJsonFeeder))]
    [MemberData(nameof(OpcodeF4JsonFeeder))]
    [MemberData(nameof(OpcodeFAJsonFeeder))]
    [MemberData(nameof(OpcodeFCJsonFeeder))]
    #if !NESDEV
    [MemberData(nameof(Opcode0CJsonFeeder))]
    [MemberData(nameof(Opcode5CJsonFeeder))]
    [MemberData(nameof(OpcodeDCJsonFeeder))]
    #endif
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

    public static IEnumerable<object[]> Opcode1CJsonFeeder()
    {
        /* common    */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "1c c7 dc", "initial": { "pc": 3457, "s": 46, "a": 178, "x": 53, "y": 153, "p": 103, "ram": [ [3457, 28], [3458, 199], [3459, 220], [56572, 166], [3460, 57]]}, "final": { "pc": 3460, "s": 46, "a": 178, "x": 53, "y": 153, "p": 103, "ram": [ [3457, 28], [3458, 199], [3459, 220], [3460, 57], [56572, 166]]}, "cycles": [ [3457, 28, "read"], [3458, 199, "read"], [3459, 220, "read"], [56572, 166, "read"]] }""") };
#if !NESDEV
        /* page jump */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "1c a0 fc", "initial": { "pc": 52873, "s": 168, "a": 169, "x": 245, "y": 31, "p": 98, "ram": [ [52873, 28], [52874, 160], [52875, 252], [64661, 108], [52876, 246]]}, "final": { "pc": 52876, "s": 168, "a": 169, "x": 245, "y": 31, "p": 98, "ram": [ [52873, 28], [52874, 160], [52875, 252], [52876, 246], [64661, 108]]}, "cycles": [ [52873, 28, "read"], [52874, 160, "read"], [52875, 252, "read"], [64661, 108, "read"]] }""") };
#endif
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
        /*   0  */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "3c 05 a9", "initial": { "pc": 13413, "s": 129, "a": 47, "x": 236, "y": 143, "p": 44, "ram": [ [13413, 60], [13414, 5], [13415, 169], [43505, 167], [13416, 212]]}, "final": { "pc": 13416, "s": 129, "a": 47, "x": 236, "y": 143, "p": 44, "ram": [ [13413, 60], [13414, 5], [13415, 169], [13416, 212], [43505, 167]]}, "cycles": [ [13413, 60, "read"], [13414, 5, "read"], [13415, 169, "read"], [43505, 167, "read"]] }""") };
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
        /*   0  */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "5c cd f2", "initial": { "pc": 24410, "s": 39, "a": 62, "x": 105, "y": 235, "p": 166, "ram": [ [24410, 92], [24411, 205], [24412, 242], [62006, 38], [24413, 84]]}, "final": { "pc": 24413, "s": 39, "a": 62, "x": 105, "y": 235, "p": 166, "ram": [ [24410, 92], [24411, 205], [24412, 242], [24413, 84], [62006, 38]]}, "cycles": [ [24410, 92, "read"], [24411, 205, "read"], [24412, 242, "read"], [62006, 38, "read"]] }""") };
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
        /*   0  */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "7c 86 26", "initial": { "pc": 42239, "s": 90, "a": 32, "x": 73, "y": 98, "p": 44, "ram": [ [42239, 124], [42240, 134], [42241, 38], [9935, 128], [42242, 88]]}, "final": { "pc": 42242, "s": 90, "a": 32, "x": 73, "y": 98, "p": 44, "ram": [ [9935, 128], [42239, 124], [42240, 134], [42241, 38], [42242, 88]]}, "cycles": [ [42239, 124, "read"], [42240, 134, "read"], [42241, 38, "read"], [9935, 128, "read"]] }""") };
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
        /*   0  */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "dc 7d 85", "initial": { "pc": 26594, "s": 167, "a": 163, "x": 169, "y": 181, "p": 98, "ram": [ [26594, 220], [26595, 125], [26596, 133], [34086, 123], [26597, 211]]}, "final": { "pc": 26597, "s": 167, "a": 163, "x": 169, "y": 181, "p": 98, "ram": [ [26594, 220], [26595, 125], [26596, 133], [26597, 211], [34086, 123]]}, "cycles": [ [26594, 220, "read"], [26595, 125, "read"], [26596, 133, "read"], [34086, 123, "read"]] }""") };
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
        /*   0  */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "fc 53 e3", "initial": { "pc": 17393, "s": 188, "a": 238, "x": 128, "y": 74, "p": 41, "ram": [ [17393, 252], [17394, 83], [17395, 227], [58323, 247], [17396, 89]]}, "final": { "pc": 17396, "s": 188, "a": 238, "x": 128, "y": 74, "p": 41, "ram": [ [17393, 252], [17394, 83], [17395, 227], [17396, 89], [58323, 247]]}, "cycles": [ [17393, 252, "read"], [17394, 83, "read"], [17395, 227, "read"], [58323, 247, "read"]] }""") };
    }
}