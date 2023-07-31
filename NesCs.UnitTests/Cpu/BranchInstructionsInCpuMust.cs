using NesCs.Common.Tests;

namespace NesCs.UnitTests.Cpu;

public class BranchInstructionsInCpuMust
{
    [Theory]
    [MemberData(nameof(Opcode30JsonFeeder))]
    [MemberData(nameof(Opcode90JsonFeeder))]
    [MemberData(nameof(OpcodeB0JsonFeeder))]
    [MemberData(nameof(OpcodeF0JsonFeeder))]
    public void BeExecutedCorrectly(SampleCpu sampleCpu)
    {
        var trace = new List<(int, byte, string)>();
        var sut = Utilities.CreateSubjectUnderTestFromSample(sampleCpu.Opcodes, sampleCpu.Initial, trace);
        sut.Run();

        Utilities.Equal(sampleCpu.Final, sut);
        Utilities.Equal(sampleCpu.Cycles, trace);
    }

    public static IEnumerable<object[]> Opcode30JsonFeeder()
    {

        // TODO: Missing negative pc test, none found in b0.json
        /* no jump                       */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "30 e3 c6", "initial": { "pc": 6629, "s": 67, "a": 153, "x": 104, "y": 127, "p": 43, "ram": [ [6629, 48], [6630, 227], [6631, 198]]}, "final": { "pc": 6631, "s": 67, "a": 153, "x": 104, "y": 127, "p": 43, "ram": [ [6629, 48], [6630, 227], [6631, 198]]}, "cycles": [ [6629, 48, "read"], [6630, 227, "read"]] }""") };
        /* jump backward to same page    */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "30 fd 0f", "initial": { "pc": 47559, "s": 63, "a": 159, "x": 144, "y": 6, "p": 174, "ram": [ [47559, 48], [47560, 253], [47561, 15], [47558, 97]]}, "final": { "pc": 47558, "s": 63, "a": 159, "x": 144, "y": 6, "p": 174, "ram": [ [47558, 97], [47559, 48], [47560, 253], [47561, 15]]}, "cycles": [ [47559, 48, "read"], [47560, 253, "read"], [47561, 15, "read"]] }""") };
        /* jump backward to another page */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "30 c0 44", "initial": { "pc": 28966, "s": 140, "a": 182, "x": 107, "y": 138, "p": 175, "ram": [ [28966, 48], [28967, 192], [28968, 68], [29160, 153], [28904, 214]]}, "final": { "pc": 28904, "s": 140, "a": 182, "x": 107, "y": 138, "p": 175, "ram": [ [28904, 214], [28966, 48], [28967, 192], [28968, 68], [29160, 153]]}, "cycles": [ [28966, 48, "read"], [28967, 192, "read"], [28968, 68, "read"], [29160, 153, "read"]] }""") };
        /* jump forward to same page     */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "30 66 b3", "initial": { "pc": 39754, "s": 28, "a": 72, "x": 92, "y": 202, "p": 232, "ram": [ [39754, 48], [39755, 102], [39756, 179], [39858, 29]]}, "final": { "pc": 39858, "s": 28, "a": 72, "x": 92, "y": 202, "p": 232, "ram": [ [39754, 48], [39755, 102], [39756, 179], [39858, 29]]}, "cycles": [ [39754, 48, "read"], [39755, 102, "read"], [39756, 179, "read"]] }""") };
        /* jump forward to another page  */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "30 60 08", "initial": { "pc": 41700, "s": 11, "a": 180, "x": 148, "y": 222, "p": 234, "ram": [ [41700, 48], [41701, 96], [41702, 8], [41542, 100], [41798, 146]]}, "final": { "pc": 41798, "s": 11, "a": 180, "x": 148, "y": 222, "p": 234, "ram": [ [41542, 100], [41700, 48], [41701, 96], [41702, 8], [41798, 146]]}, "cycles": [ [41700, 48, "read"], [41701, 96, "read"], [41702, 8, "read"], [41542, 100, "read"]] }""") };
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

    public static IEnumerable<object[]> OpcodeF0JsonFeeder()
    {
        /* no jump                       */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "f0 b8 e3", "initial": { "pc": 62453, "s": 196, "a": 69, "x": 35, "y": 199, "p": 32, "ram": [ [62453, 240], [62454, 184], [62455, 227]]}, "final": { "pc": 62455, "s": 196, "a": 69, "x": 35, "y": 199, "p": 32, "ram": [ [62453, 240], [62454, 184], [62455, 227]]}, "cycles": [ [62453, 240, "read"], [62454, 184, "read"]] }""") };
        /* jump backward to another page */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "f0 a7 19", "initial": { "pc": 29268, "s": 59, "a": 0, "x": 185, "y": 180, "p": 163, "ram": [ [29268, 240], [29269, 167], [29270, 25], [29437, 180], [29181, 139]]}, "final": { "pc": 29181, "s": 59, "a": 0, "x": 185, "y": 180, "p": 163, "ram": [ [29181, 139], [29268, 240], [29269, 167], [29270, 25], [29437, 180]]}, "cycles": [ [29268, 240, "read"], [29269, 167, "read"], [29270, 25, "read"], [29437, 180, "read"]] }""") };
        /* jump forward to another page  */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "f0 7b 5c", "initial": { "pc": 30638, "s": 202, "a": 241, "x": 14, "y": 222, "p": 46, "ram": [ [30638, 240], [30639, 123], [30640, 92], [30507, 160], [30763, 223]]}, "final": { "pc": 30763, "s": 202, "a": 241, "x": 14, "y": 222, "p": 46, "ram": [ [30507, 160], [30638, 240], [30639, 123], [30640, 92], [30763, 223]]}, "cycles": [ [30638, 240, "read"], [30639, 123, "read"], [30640, 92, "read"], [30507, 160, "read"]] }""") };
        /* jump backward in same page    */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "f0 bb 37", "initial": { "pc": 19544, "s": 11, "a": 27, "x": 22, "y": 154, "p": 42, "ram": [ [19544, 240], [19545, 187], [19546, 55], [19477, 185]]}, "final": { "pc": 19477, "s": 11, "a": 27, "x": 22, "y": 154, "p": 42, "ram": [ [19477, 185], [19544, 240], [19545, 187], [19546, 55]]}, "cycles": [ [19544, 240, "read"], [19545, 187, "read"], [19546, 55, "read"]] }""") };
        /* negative pc                   */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "f0 8a d9", "initial": { "pc": 84, "s": 243, "a": 128, "x": 175, "y": 221, "p": 234, "ram": [ [84, 240], [85, 138], [86, 217], [224, 142], [65504, 202]]}, "final": { "pc": 65504, "s": 243, "a": 128, "x": 175, "y": 221, "p": 234, "ram": [ [84, 240], [85, 138], [86, 217], [224, 142], [65504, 202]]}, "cycles": [ [84, 240, "read"], [85, 138, "read"], [86, 217, "read"], [224, 142, "read"]] }""") };
    }
}