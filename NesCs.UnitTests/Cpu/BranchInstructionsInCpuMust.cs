using NesCs.Common.Tests;

namespace NesCs.UnitTests.Cpu;

public class BranchInstructionsInCpuMust
{
    [Theory]
    [MemberData(nameof(Opcode10JsonFeeder))]
    [MemberData(nameof(Opcode20JsonFeeder))]
    [MemberData(nameof(Opcode30JsonFeeder))]
    [MemberData(nameof(Opcode4CJsonFeeder))]
    [MemberData(nameof(Opcode50JsonFeeder))]
    [MemberData(nameof(Opcode6CJsonFeeder))]
    [MemberData(nameof(Opcode70JsonFeeder))]
    [MemberData(nameof(Opcode90JsonFeeder))]
    [MemberData(nameof(OpcodeB0JsonFeeder))]
    [MemberData(nameof(OpcodeD0JsonFeeder))]
    [MemberData(nameof(OpcodeF0JsonFeeder))]
    public void BeExecutedCorrectly(SampleCpu sampleCpu)
    {
        var trace = new List<(int, byte, string)>();
        var sut = Utilities.CreateSubjectUnderTestFromSample(sampleCpu.Opcodes, sampleCpu.Initial, trace);
        sut.Run();

        Utilities.Equal(sampleCpu.Final, sut);
        Utilities.Equal(sampleCpu.Cycles, trace);
    }

    // TODO: Missing negative pc test, none found in b0.json
    public static IEnumerable<object[]> Opcode10JsonFeeder()
    {
        /* no jump                       */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "10 91 3e", "initial": { "pc": 53844, "s": 183, "a": 60, "x": 87, "y": 196, "p": 227, "ram": [ [53844, 16], [53845, 145], [53846, 62]]}, "final": { "pc": 53846, "s": 183, "a": 60, "x": 87, "y": 196, "p": 227, "ram": [ [53844, 16], [53845, 145], [53846, 62]]}, "cycles": [ [53844, 16, "read"], [53845, 145, "read"]] }""") };
        /* jump backward to same page    */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "10 98 49", "initial": { "pc": 41379, "s": 218, "a": 248, "x": 28, "y": 116, "p": 32, "ram": [ [41379, 16], [41380, 152], [41381, 73], [41277, 175]]}, "final": { "pc": 41277, "s": 218, "a": 248, "x": 28, "y": 116, "p": 32, "ram": [ [41277, 175], [41379, 16], [41380, 152], [41381, 73]]}, "cycles": [ [41379, 16, "read"], [41380, 152, "read"], [41381, 73, "read"]] }""") };
        /* jump backward to another page */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "10 b3 96", "initial": { "pc": 55085, "s": 149, "a": 104, "x": 40, "y": 51, "p": 43, "ram": [ [55085, 16], [55086, 179], [55087, 150], [55266, 80], [55010, 179]]}, "final": { "pc": 55010, "s": 149, "a": 104, "x": 40, "y": 51, "p": 43, "ram": [ [55010, 179], [55085, 16], [55086, 179], [55087, 150], [55266, 80]]}, "cycles": [ [55085, 16, "read"], [55086, 179, "read"], [55087, 150, "read"], [55266, 80, "read"]] }""") };
        /* jump forward to same page     */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "10 3e 16", "initial": { "pc": 33557, "s": 62, "a": 53, "x": 232, "y": 229, "p": 35, "ram": [ [33557, 16], [33558, 62], [33559, 22], [33621, 95]]}, "final": { "pc": 33621, "s": 62, "a": 53, "x": 232, "y": 229, "p": 35, "ram": [ [33557, 16], [33558, 62], [33559, 22], [33621, 95]]}, "cycles": [ [33557, 16, "read"], [33558, 62, "read"], [33559, 22, "read"]] }""") };
        /* jump forward to another page  */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "10 20 b7", "initial": { "pc": 54520, "s": 220, "a": 193, "x": 219, "y": 144, "p": 104, "ram": [ [54520, 16], [54521, 32], [54522, 183], [54298, 189], [54554, 94]]}, "final": { "pc": 54554, "s": 220, "a": 193, "x": 219, "y": 144, "p": 104, "ram": [ [54298, 189], [54520, 16], [54521, 32], [54522, 183], [54554, 94]]}, "cycles": [ [54520, 16, "read"], [54521, 32, "read"], [54522, 183, "read"], [54298, 189, "read"]] }""") };
    }

    public static IEnumerable<object[]> Opcode20JsonFeeder()
    {
        /* common jump       */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "20 ab 9d", "initial": { "pc": 39103, "s": 234, "a": 133, "x": 81, "y": 242, "p": 103, "ram": [ [39103, 32], [39104, 171], [39105, 157], [490, 195], [40363, 181]]}, "final": { "pc": 40363, "s": 232, "a": 133, "x": 81, "y": 242, "p": 103, "ram": [ [489, 193], [490, 152], [39103, 32], [39104, 171], [39105, 157], [40363, 181]]}, "cycles": [ [39103, 32, "read"], [39104, 171, "read"], [490, 195, "read"], [490, 152, "write"], [489, 193, "write"], [39105, 157, "read"]] }""") };
        /* low byte overflow */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "20 36 97", "initial": { "pc": 16126, "s": 89, "a": 35, "x": 56, "y": 187, "p": 98, "ram": [ [16126, 32], [16127, 54], [16128, 151], [345, 136], [38710, 39]]}, "final": { "pc": 38710, "s": 87, "a": 35, "x": 56, "y": 187, "p": 98, "ram": [ [344, 0], [345, 63], [16126, 32], [16127, 54], [16128, 151], [38710, 39]]}, "cycles": [ [16126, 32, "read"], [16127, 54, "read"], [345, 136, "read"], [345, 63, "write"], [344, 0, "write"], [16128, 151, "read"]] }""") };
    }

    // TODO: Missing negative pc test, none found in b0.json
    public static IEnumerable<object[]> Opcode30JsonFeeder()
    {
        /* no jump                       */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "30 e3 c6", "initial": { "pc": 6629, "s": 67, "a": 153, "x": 104, "y": 127, "p": 43, "ram": [ [6629, 48], [6630, 227], [6631, 198]]}, "final": { "pc": 6631, "s": 67, "a": 153, "x": 104, "y": 127, "p": 43, "ram": [ [6629, 48], [6630, 227], [6631, 198]]}, "cycles": [ [6629, 48, "read"], [6630, 227, "read"]] }""") };
        /* jump backward to same page    */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "30 fd 0f", "initial": { "pc": 47559, "s": 63, "a": 159, "x": 144, "y": 6, "p": 174, "ram": [ [47559, 48], [47560, 253], [47561, 15], [47558, 97]]}, "final": { "pc": 47558, "s": 63, "a": 159, "x": 144, "y": 6, "p": 174, "ram": [ [47558, 97], [47559, 48], [47560, 253], [47561, 15]]}, "cycles": [ [47559, 48, "read"], [47560, 253, "read"], [47561, 15, "read"]] }""") };
        /* jump backward to another page */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "30 c0 44", "initial": { "pc": 28966, "s": 140, "a": 182, "x": 107, "y": 138, "p": 175, "ram": [ [28966, 48], [28967, 192], [28968, 68], [29160, 153], [28904, 214]]}, "final": { "pc": 28904, "s": 140, "a": 182, "x": 107, "y": 138, "p": 175, "ram": [ [28904, 214], [28966, 48], [28967, 192], [28968, 68], [29160, 153]]}, "cycles": [ [28966, 48, "read"], [28967, 192, "read"], [28968, 68, "read"], [29160, 153, "read"]] }""") };
        /* jump forward to same page     */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "30 66 b3", "initial": { "pc": 39754, "s": 28, "a": 72, "x": 92, "y": 202, "p": 232, "ram": [ [39754, 48], [39755, 102], [39756, 179], [39858, 29]]}, "final": { "pc": 39858, "s": 28, "a": 72, "x": 92, "y": 202, "p": 232, "ram": [ [39754, 48], [39755, 102], [39756, 179], [39858, 29]]}, "cycles": [ [39754, 48, "read"], [39755, 102, "read"], [39756, 179, "read"]] }""") };
        /* jump forward to another page  */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "30 60 08", "initial": { "pc": 41700, "s": 11, "a": 180, "x": 148, "y": 222, "p": 234, "ram": [ [41700, 48], [41701, 96], [41702, 8], [41542, 100], [41798, 146]]}, "final": { "pc": 41798, "s": 11, "a": 180, "x": 148, "y": 222, "p": 234, "ram": [ [41542, 100], [41700, 48], [41701, 96], [41702, 8], [41798, 146]]}, "cycles": [ [41700, 48, "read"], [41701, 96, "read"], [41702, 8, "read"], [41542, 100, "read"]] }""") };
    }

    public static IEnumerable<object[]> Opcode4CJsonFeeder()
    {
        /*   0  */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "4c 7a 8b", "initial": { "pc": 48659, "s": 156, "a": 226, "x": 92, "y": 82, "p": 44, "ram": [ [48659, 76], [48660, 122], [48661, 139], [35706, 107]]}, "final": { "pc": 35706, "s": 156, "a": 226, "x": 92, "y": 82, "p": 44, "ram": [ [35706, 107], [48659, 76], [48660, 122], [48661, 139]]}, "cycles": [ [48659, 76, "read"], [48660, 122, "read"], [48661, 139, "read"]] }""") };
    }

    public static IEnumerable<object[]> Opcode50JsonFeeder()
    {
        /*   0  */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "50 28 c4", "initial": { "pc": 45493, "s": 30, "a": 42, "x": 168, "y": 209, "p": 108, "ram": [ [45493, 80], [45494, 40], [45495, 196]]}, "final": { "pc": 45495, "s": 30, "a": 42, "x": 168, "y": 209, "p": 108, "ram": [ [45493, 80], [45494, 40], [45495, 196]]}, "cycles": [ [45493, 80, "read"], [45494, 40, "read"]] }""") };
    }

    public static IEnumerable<object[]> Opcode6CJsonFeeder()
    {
        /* common            */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "6c a5 12", "initial": { "pc": 60280, "s": 77, "a": 148, "x": 227, "y": 97, "p": 168, "ram": [ [60280, 108], [60281, 165], [60282, 18], [4773, 80], [4774, 37], [9552, 104]]}, "final": { "pc": 9552, "s": 77, "a": 148, "x": 227, "y": 97, "p": 168, "ram": [ [4773, 80], [4774, 37], [9552, 104], [60280, 108], [60281, 165], [60282, 18]]}, "cycles": [ [60280, 108, "read"], [60281, 165, "read"], [60282, 18, "read"], [4773, 80, "read"], [4774, 37, "read"]] }""") };
        /* 6502 indirect bug */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "6c ff b4", "initial": { "pc": 60908, "s": 168, "a": 133, "x": 33, "y": 251, "p": 171, "ram": [ [60908, 108], [60909, 255], [60910, 180], [46335, 198], [46080, 76], [19654, 56]]}, "final": { "pc": 19654, "s": 168, "a": 133, "x": 33, "y": 251, "p": 171, "ram": [ [19654, 56], [46080, 76], [46335, 198], [60908, 108], [60909, 255], [60910, 180]]}, "cycles": [ [60908, 108, "read"], [60909, 255, "read"], [60910, 180, "read"], [46335, 198, "read"], [46080, 76, "read"]] }""") };
    }

    // TODO: Missing negative pc test, none found in b0.json
    public static IEnumerable<object[]> Opcode70JsonFeeder()
    {
        /* no jump                       */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "70 46 ad", "initial": { "pc": 22193, "s": 172, "a": 83, "x": 254, "y": 72, "p": 173, "ram": [ [22193, 112], [22194, 70], [22195, 173]]}, "final": { "pc": 22195, "s": 172, "a": 83, "x": 254, "y": 72, "p": 173, "ram": [ [22193, 112], [22194, 70], [22195, 173]]}, "cycles": [ [22193, 112, "read"], [22194, 70, "read"]] }""") };
        /* jump forward to same page     */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "70 e6 a3", "initial": { "pc": 12337, "s": 65, "a": 154, "x": 94, "y": 206, "p": 238, "ram": [ [12337, 112], [12338, 230], [12339, 163], [12313, 78]]}, "final": { "pc": 12313, "s": 65, "a": 154, "x": 94, "y": 206, "p": 238, "ram": [ [12313, 78], [12337, 112], [12338, 230], [12339, 163]]}, "cycles": [ [12337, 112, "read"], [12338, 230, "read"], [12339, 163, "read"]] }""") };
        /* jump forward to another page  */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "70 7e 66", "initial": { "pc": 15082, "s": 172, "a": 162, "x": 26, "y": 192, "p": 111, "ram": [ [15082, 112], [15083, 126], [15084, 102], [14954, 6], [15210, 108]]}, "final": { "pc": 15210, "s": 172, "a": 162, "x": 26, "y": 192, "p": 111, "ram": [ [14954, 6], [15082, 112], [15083, 126], [15084, 102], [15210, 108]]}, "cycles": [ [15082, 112, "read"], [15083, 126, "read"], [15084, 102, "read"], [14954, 6, "read"]] }""") };
        /* jump backward to same page    */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "70 9a b6", "initial": { "pc": 18342, "s": 58, "a": 96, "x": 248, "y": 241, "p": 227, "ram": [ [18342, 112], [18343, 154], [18344, 182], [18242, 199]]}, "final": { "pc": 18242, "s": 58, "a": 96, "x": 248, "y": 241, "p": 227, "ram": [ [18242, 199], [18342, 112], [18343, 154], [18344, 182]]}, "cycles": [ [18342, 112, "read"], [18343, 154, "read"], [18344, 182, "read"]] }""") };
        /* jump backward to another page */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "70 c7 d9", "initial": { "pc": 46870, "s": 158, "a": 10, "x": 216, "y": 11, "p": 101, "ram": [ [46870, 112], [46871, 199], [46872, 217], [47071, 32], [46815, 58]]}, "final": { "pc": 46815, "s": 158, "a": 10, "x": 216, "y": 11, "p": 101, "ram": [ [46815, 58], [46870, 112], [46871, 199], [46872, 217], [47071, 32]]}, "cycles": [ [46870, 112, "read"], [46871, 199, "read"], [46872, 217, "read"], [47071, 32, "read"]] }""") };
    }

    public static IEnumerable<object[]> Opcode90JsonFeeder()
    {
        /* no jump                       */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "90 7e 9a", "initial": { "pc": 54081, "s": 174, "a": 120, "x": 57, "y": 25, "p": 231, "ram": [ [54081, 144], [54082, 126], [54083, 154]]}, "final": { "pc": 54083, "s": 174, "a": 120, "x": 57, "y": 25, "p": 231, "ram": [ [54081, 144], [54082, 126], [54083, 154]]}, "cycles": [ [54081, 144, "read"], [54082, 126, "read"]] }""") };
        /* jump backward to same page    */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "90 d5 63", "initial": { "pc": 6021, "s": 62, "a": 115, "x": 181, "y": 52, "p": 224, "ram": [ [6021, 144], [6022, 213], [6023, 99], [5980, 104]]}, "final": { "pc": 5980, "s": 62, "a": 115, "x": 181, "y": 52, "p": 224, "ram": [ [5980, 104], [6021, 144], [6022, 213], [6023, 99]]}, "cycles": [ [6021, 144, "read"], [6022, 213, "read"], [6023, 99, "read"]] }""") };
        /* jump backward to another page */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "90 91 aa", "initial": { "pc": 41048, "s": 47, "a": 116, "x": 174, "y": 163, "p": 224, "ram": [ [41048, 144], [41049, 145], [41050, 170], [41195, 233], [40939, 101]]}, "final": { "pc": 40939, "s": 47, "a": 116, "x": 174, "y": 163, "p": 224, "ram": [ [40939, 101], [41048, 144], [41049, 145], [41050, 170], [41195, 233]]}, "cycles": [ [41048, 144, "read"], [41049, 145, "read"], [41050, 170, "read"], [41195, 233, "read"]] }""") };
        /* jump forward to same page     */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "90 7a 27", "initial": { "pc": 385, "s": 33, "a": 108, "x": 82, "y": 253, "p": 172, "ram": [ [385, 144], [386, 122], [387, 39], [509, 228]]}, "final": { "pc": 509, "s": 33, "a": 108, "x": 82, "y": 253, "p": 172, "ram": [ [385, 144], [386, 122], [387, 39], [509, 228]]}, "cycles": [ [385, 144, "read"], [386, 122, "read"], [387, 39, "read"]] }""") };
        /* jump forward to another page  */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "90 37 b7", "initial": { "pc": 25549, "s": 96, "a": 231, "x": 233, "y": 68, "p": 224, "ram": [ [25549, 144], [25550, 55], [25551, 183], [25350, 175], [25606, 140]]}, "final": { "pc": 25606, "s": 96, "a": 231, "x": 233, "y": 68, "p": 224, "ram": [ [25350, 175], [25549, 144], [25550, 55], [25551, 183], [25606, 140]]}, "cycles": [ [25549, 144, "read"], [25550, 55, "read"], [25551, 183, "read"], [25350, 175, "read"]] }""") };
        /* negative pc                   */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "90 5e c3", "initial": { "pc": 65515, "s": 72, "a": 88, "x": 194, "y": 232, "p": 170, "ram": [ [65515, 144], [65516, 94], [65517, 195], [65355, 124], [75, 76]]}, "final": { "pc": 75, "s": 72, "a": 88, "x": 194, "y": 232, "p": 170, "ram": [ [75, 76], [65355, 124], [65515, 144], [65516, 94], [65517, 195]]}, "cycles": [ [65515, 144, "read"], [65516, 94, "read"], [65517, 195, "read"], [65355, 124, "read"]] }""") };
    }

    // TODO: Missing negative pc test, none found in b0.json
    public static IEnumerable<object[]> OpcodeB0JsonFeeder()
    {
        /* no jump                       */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "b0 dc 98", "initial": { "pc": 20980, "s": 156, "a": 116, "x": 48, "y": 133, "p": 232, "ram": [ [20980, 176], [20981, 220], [20982, 152]]}, "final": { "pc": 20982, "s": 156, "a": 116, "x": 48, "y": 133, "p": 232, "ram": [ [20980, 176], [20981, 220], [20982, 152]]}, "cycles": [ [20980, 176, "read"], [20981, 220, "read"]] }""") };
        /* jump backward to same page    */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "b0 da cd", "initial": { "pc": 16337, "s": 156, "a": 188, "x": 155, "y": 109, "p": 175, "ram": [ [16337, 176], [16338, 218], [16339, 205], [16301, 146]]}, "final": { "pc": 16301, "s": 156, "a": 188, "x": 155, "y": 109, "p": 175, "ram": [ [16301, 146], [16337, 176], [16338, 218], [16339, 205]]}, "cycles": [ [16337, 176, "read"], [16338, 218, "read"], [16339, 205, "read"]] }""") };
        /* jump backward to another page */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "b0 bd ff", "initial": { "pc": 14080, "s": 3, "a": 1, "x": 249, "y": 62, "p": 173, "ram": [ [14080, 176], [14081, 189], [14082, 255], [14271, 6], [14015, 240]]}, "final": { "pc": 14015, "s": 3, "a": 1, "x": 249, "y": 62, "p": 173, "ram": [ [14015, 240], [14080, 176], [14081, 189], [14082, 255], [14271, 6]]}, "cycles": [ [14080, 176, "read"], [14081, 189, "read"], [14082, 255, "read"], [14271, 6, "read"]] }""") };
        /* jump forward to same page     */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "b0 20 8a", "initial": { "pc": 19652, "s": 57, "a": 75, "x": 4, "y": 80, "p": 233, "ram": [ [19652, 176], [19653, 32], [19654, 138], [19686, 44]]}, "final": { "pc": 19686, "s": 57, "a": 75, "x": 4, "y": 80, "p": 233, "ram": [ [19652, 176], [19653, 32], [19654, 138], [19686, 44]]}, "cycles": [ [19652, 176, "read"], [19653, 32, "read"], [19654, 138, "read"]] }""") };
        /* jump forward to another page  */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "b0 56 8b", "initial": { "pc": 434, "s": 18, "a": 243, "x": 85, "y": 26, "p": 97, "ram": [ [434, 176], [435, 86], [436, 139], [266, 98], [522, 93]]}, "final": { "pc": 522, "s": 18, "a": 243, "x": 85, "y": 26, "p": 97, "ram": [ [266, 98], [434, 176], [435, 86], [436, 139], [522, 93]]}, "cycles": [ [434, 176, "read"], [435, 86, "read"], [436, 139, "read"], [266, 98, "read"]] }""") };
    }

    public static IEnumerable<object[]> OpcodeD0JsonFeeder()
    {
        /* no jump                       */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "d0 2b 47", "initial": { "pc": 23233, "s": 27, "a": 77, "x": 97, "y": 67, "p": 226, "ram": [ [23233, 208], [23234, 43], [23235, 71]]}, "final": { "pc": 23235, "s": 27, "a": 77, "x": 97, "y": 67, "p": 226, "ram": [ [23233, 208], [23234, 43], [23235, 71]]}, "cycles": [ [23233, 208, "read"], [23234, 43, "read"]] }""") };
        /* jump backward to same page    */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "d0 db 70", "initial": { "pc": 54820, "s": 239, "a": 190, "x": 1, "y": 21, "p": 236, "ram": [ [54820, 208], [54821, 219], [54822, 112], [54785, 6]]}, "final": { "pc": 54785, "s": 239, "a": 190, "x": 1, "y": 21, "p": 236, "ram": [ [54785, 6], [54820, 208], [54821, 219], [54822, 112]]}, "cycles": [ [54820, 208, "read"], [54821, 219, "read"], [54822, 112, "read"]] }""") };
        /* jump backward to another page */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "d0 c6 5d", "initial": { "pc": 30481, "s": 88, "a": 112, "x": 217, "y": 229, "p": 96, "ram": [ [30481, 208], [30482, 198], [30483, 93], [30681, 93], [30425, 60]]}, "final": { "pc": 30425, "s": 88, "a": 112, "x": 217, "y": 229, "p": 96, "ram": [ [30425, 60], [30481, 208], [30482, 198], [30483, 93], [30681, 93]]}, "cycles": [ [30481, 208, "read"], [30482, 198, "read"], [30483, 93, "read"], [30681, 93, "read"]] }""") };
        /* jump forward to same page     */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "d0 09 a5", "initial": { "pc": 5070, "s": 222, "a": 101, "x": 214, "y": 176, "p": 173, "ram": [ [5070, 208], [5071, 9], [5072, 165], [5081, 57]]}, "final": { "pc": 5081, "s": 222, "a": 101, "x": 214, "y": 176, "p": 173, "ram": [ [5070, 208], [5071, 9], [5072, 165], [5081, 57]]}, "cycles": [ [5070, 208, "read"], [5071, 9, "read"], [5072, 165, "read"]] }""") };
        /* jump forward to another page  */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "d0 2e b0", "initial": { "pc": 55801, "s": 142, "a": 57, "x": 123, "y": 136, "p": 109, "ram": [ [55801, 208], [55802, 46], [55803, 176], [55593, 50], [55849, 185]]}, "final": { "pc": 55849, "s": 142, "a": 57, "x": 123, "y": 136, "p": 109, "ram": [ [55593, 50], [55801, 208], [55802, 46], [55803, 176], [55849, 185]]}, "cycles": [ [55801, 208, "read"], [55802, 46, "read"], [55803, 176, "read"], [55593, 50, "read"]] }""") };
        /* negative pc                   */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "d0 36 f1", "initial": { "pc": 65532, "s": 63, "a": 188, "x": 72, "y": 235, "p": 100, "ram": [ [65532, 208], [65533, 54], [65534, 241], [65332, 80], [52, 154]]}, "final": { "pc": 52, "s": 63, "a": 188, "x": 72, "y": 235, "p": 100, "ram": [ [52, 154], [65332, 80], [65532, 208], [65533, 54], [65534, 241]]}, "cycles": [ [65532, 208, "read"], [65533, 54, "read"], [65534, 241, "read"], [65332, 80, "read"]] }""") };
    }

    public static IEnumerable<object[]> OpcodeF0JsonFeeder()
    {
        /* no jump                       */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "f0 b8 e3", "initial": { "pc": 62453, "s": 196, "a": 69, "x": 35, "y": 199, "p": 32, "ram": [ [62453, 240], [62454, 184], [62455, 227]]}, "final": { "pc": 62455, "s": 196, "a": 69, "x": 35, "y": 199, "p": 32, "ram": [ [62453, 240], [62454, 184], [62455, 227]]}, "cycles": [ [62453, 240, "read"], [62454, 184, "read"]] }""") };
        /* jump backward to another page */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "f0 a7 19", "initial": { "pc": 29268, "s": 59, "a": 0, "x": 185, "y": 180, "p": 163, "ram": [ [29268, 240], [29269, 167], [29270, 25], [29437, 180], [29181, 139]]}, "final": { "pc": 29181, "s": 59, "a": 0, "x": 185, "y": 180, "p": 163, "ram": [ [29181, 139], [29268, 240], [29269, 167], [29270, 25], [29437, 180]]}, "cycles": [ [29268, 240, "read"], [29269, 167, "read"], [29270, 25, "read"], [29437, 180, "read"]] }""") };
        /* jump forward to another page  */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "f0 7b 5c", "initial": { "pc": 30638, "s": 202, "a": 241, "x": 14, "y": 222, "p": 46, "ram": [ [30638, 240], [30639, 123], [30640, 92], [30507, 160], [30763, 223]]}, "final": { "pc": 30763, "s": 202, "a": 241, "x": 14, "y": 222, "p": 46, "ram": [ [30507, 160], [30638, 240], [30639, 123], [30640, 92], [30763, 223]]}, "cycles": [ [30638, 240, "read"], [30639, 123, "read"], [30640, 92, "read"], [30507, 160, "read"]] }""") };
        /* jump backward in same page    */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "f0 bb 37", "initial": { "pc": 19544, "s": 11, "a": 27, "x": 22, "y": 154, "p": 42, "ram": [ [19544, 240], [19545, 187], [19546, 55], [19477, 185]]}, "final": { "pc": 19477, "s": 11, "a": 27, "x": 22, "y": 154, "p": 42, "ram": [ [19477, 185], [19544, 240], [19545, 187], [19546, 55]]}, "cycles": [ [19544, 240, "read"], [19545, 187, "read"], [19546, 55, "read"]] }""") };
        /* negative pc                   */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "f0 8a d9", "initial": { "pc": 84, "s": 243, "a": 128, "x": 175, "y": 221, "p": 234, "ram": [ [84, 240], [85, 138], [86, 217], [224, 142], [65504, 202]]}, "final": { "pc": 65504, "s": 243, "a": 128, "x": 175, "y": 221, "p": 234, "ram": [ [84, 240], [85, 138], [86, 217], [224, 142], [65504, 202]]}, "cycles": [ [84, 240, "read"], [85, 138, "read"], [86, 217, "read"], [224, 142, "read"]] }""") };
    }
}