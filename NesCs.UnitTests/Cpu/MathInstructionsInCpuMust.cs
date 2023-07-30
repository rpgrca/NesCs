using NesCs.Tests.Common;

namespace NesCs.UnitTests.Cpu;

public class MathInstructionsInCpuMust
{
    [Theory]
    [MemberData(nameof(OpcodeE9JsonFeeder))]
    public void BeExecutedCorrectly(SampleCpu sampleCpu)
    {
        var trace = new List<(int, byte, string)>();
        var sut = Utilities.CreateSubjectUnderTestFromSample(sampleCpu.Opcodes, sampleCpu.Initial, trace);
        sut.Run();

        Utilities.Equal(sampleCpu.Final, sut);
        Utilities.Equal(sampleCpu.Cycles, trace);
    }

    public static IEnumerable<object[]> OpcodeE9JsonFeeder()
    {
        /*   0      */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "e9 99 00", "initial": { "pc": 46693, "s": 140, "a": 102, "x": 169, "y": 151, "p": 232, "ram": [ [46693, 233], [46694, 153], [46695, 0]]}, "final": { "pc": 46695, "s": 140, "a": 204, "x": 169, "y": 151, "p": 232, "ram": [ [46693, 233], [46694, 153], [46695, 0]]}, "cycles": [ [46693, 233, "read"], [46694, 153, "read"]] }""") };
//        /*   1 C    */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "e9 29 cc", "initial": { "pc": 62291, "s": 159, "a": 241, "x": 149, "y": 71, "p": 174, "ram": [ [62291, 233], [62292, 41], [62293, 204]]}, "final": { "pc": 62293, "s": 159, "a": 199, "x": 149, "y": 71, "p": 173, "ram": [ [62291, 233], [62292, 41], [62293, 204]]}, "cycles": [ [62291, 233, "read"], [62292, 41, "read"]] }""") };
//        /*   2 Z    */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "e9 35 cc", "initial": { "pc": 45934, "s": 162, "a": 137, "x": 49, "y": 211, "p": 111, "ram": [ [45934, 233], [45935, 53], [45936, 204]]}, "final": { "pc": 45936, "s": 162, "a": 84, "x": 49, "y": 211, "p": 109, "ram": [ [45934, 233], [45935, 53], [45936, 204]]}, "cycles": [ [45934, 233, "read"], [45935, 53, "read"]] }""") };
//        /*   3 ZC   */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "e9 df 06", "initial": { "pc": 45710, "s": 157, "a": 147, "x": 89, "y": 205, "p": 171, "ram": [ [45710, 233], [45711, 223], [45712, 6]]}, "final": { "pc": 45712, "s": 157, "a": 180, "x": 89, "y": 205, "p": 168, "ram": [ [45710, 233], [45711, 223], [45712, 6]]}, "cycles": [ [45710, 233, "read"], [45711, 223, "read"]] }""") };
//        /*  64 V    */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "e9 55 49", "initial": { "pc": 7675, "s": 115, "a": 54, "x": 222, "y": 131, "p": 228, "ram": [ [7675, 233], [7676, 85], [7677, 73]]}, "final": { "pc": 7677, "s": 115, "a": 224, "x": 222, "y": 131, "p": 164, "ram": [ [7675, 233], [7676, 85], [7677, 73]]}, "cycles": [ [7675, 233, "read"], [7676, 85, "read"]] }""") };
//        /*  65 VC   */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "e9 86 eb", "initial": { "pc": 63428, "s": 243, "a": 188, "x": 128, "y": 185, "p": 100, "ram": [ [63428, 233], [63429, 134], [63430, 235]]}, "final": { "pc": 63430, "s": 243, "a": 53, "x": 128, "y": 185, "p": 37, "ram": [ [63428, 233], [63429, 134], [63430, 235]]}, "cycles": [ [63428, 233, "read"], [63429, 134, "read"]] }""") };
//        /*  66 VZ   */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "e9 05 99", "initial": { "pc": 48311, "s": 99, "a": 244, "x": 223, "y": 207, "p": 239, "ram": [ [48311, 233], [48312, 5], [48313, 153]]}, "final": { "pc": 48313, "s": 99, "a": 239, "x": 223, "y": 207, "p": 173, "ram": [ [48311, 233], [48312, 5], [48313, 153]]}, "cycles": [ [48311, 233, "read"], [48312, 5, "read"]] }""") };
//        /*  67 VCZ  */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "e9 20 ae", "initial": { "pc": 60024, "s": 171, "a": 50, "x": 168, "y": 179, "p": 106, "ram": [ [60024, 233], [60025, 32], [60026, 174]]}, "final": { "pc": 60026, "s": 171, "a": 17, "x": 168, "y": 179, "p": 41, "ram": [ [60024, 233], [60025, 32], [60026, 174]]}, "cycles": [ [60024, 233, "read"], [60025, 32, "read"]] }""") };
//        /* 128 N    */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "e9 79 c9", "initial": { "pc": 61580, "s": 200, "a": 200, "x": 83, "y": 138, "p": 225, "ram": [ [61580, 233], [61581, 121], [61582, 201]]}, "final": { "pc": 61582, "s": 200, "a": 79, "x": 83, "y": 138, "p": 97, "ram": [ [61580, 233], [61581, 121], [61582, 201]]}, "cycles": [ [61580, 233, "read"], [61581, 121, "read"]] }""") };
//        /* 129 NC   */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "e9 1a d1", "initial": { "pc": 5608, "s": 191, "a": 124, "x": 119, "y": 135, "p": 164, "ram": [ [5608, 233], [5609, 26], [5610, 209]]}, "final": { "pc": 5610, "s": 191, "a": 97, "x": 119, "y": 135, "p": 37, "ram": [ [5608, 233], [5609, 26], [5610, 209]]}, "cycles": [ [5608, 233, "read"], [5609, 26, "read"]] }""") };
//        /* 130 NZ   */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "e9 e7 90", "initial": { "pc": 51354, "s": 81, "a": 126, "x": 142, "y": 131, "p": 102, "ram": [ [51354, 233], [51355, 231], [51356, 144]]}, "final": { "pc": 51356, "s": 81, "a": 150, "x": 142, "y": 131, "p": 228, "ram": [ [51354, 233], [51355, 231], [51356, 144]]}, "cycles": [ [51354, 233, "read"], [51355, 231, "read"]] }""") };
//        /* 131 NZC  */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "e9 fb ee", "initial": { "pc": 53112, "s": 119, "a": 107, "x": 76, "y": 167, "p": 171, "ram": [ [53112, 233], [53113, 251], [53114, 238]]}, "final": { "pc": 53114, "s": 119, "a": 112, "x": 76, "y": 167, "p": 40, "ram": [ [53112, 233], [53113, 251], [53114, 238]]}, "cycles": [ [53112, 233, "read"], [53113, 251, "read"]] }""") };
//        /* 192 NV   */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "e9 0a b6", "initial": { "pc": 19014, "s": 145, "a": 175, "x": 40, "y": 248, "p": 97, "ram": [ [19014, 233], [19015, 10], [19016, 182]]}, "final": { "pc": 19016, "s": 145, "a": 165, "x": 40, "y": 248, "p": 161, "ram": [ [19014, 233], [19015, 10], [19016, 182]]}, "cycles": [ [19014, 233, "read"], [19015, 10, "read"]] }""") };
//        /* 193 NVC  */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "e9 f0 e4", "initial": { "pc": 63335, "s": 71, "a": 170, "x": 246, "y": 27, "p": 101, "ram": [ [63335, 233], [63336, 240], [63337, 228]]}, "final": { "pc": 63337, "s": 71, "a": 186, "x": 246, "y": 27, "p": 164, "ram": [ [63335, 233], [63336, 240], [63337, 228]]}, "cycles": [ [63335, 233, "read"], [63336, 240, "read"]] }""") };
//        /* 194 NVCZ */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "e9 6e fd", "initial": { "pc": 59471, "s": 75, "a": 214, "x": 135, "y": 191, "p": 175, "ram": [ [59471, 233], [59472, 110], [59473, 253]]}, "final": { "pc": 59473, "s": 75, "a": 104, "x": 135, "y": 191, "p": 109, "ram": [ [59471, 233], [59472, 110], [59473, 253]]}, "cycles": [ [59471, 233, "read"], [59472, 110, "read"]] }""") };
    }
}
