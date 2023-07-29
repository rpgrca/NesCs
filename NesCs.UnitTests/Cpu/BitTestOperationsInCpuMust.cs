using NesCs.Tests.Common;

namespace NesCs.UnitTests.Cpu;

public class BitTestOperationsInCpuMust
{
    [Theory]
    [MemberData(nameof(Opcode2CJsonFeeder))]
    public void BeExecutedCorrectly(SampleCpu sampleCpu)
    {
        var trace = new List<(int, byte, string)>();
        var sut = Utilities.CreateSubjectUnderTestFromSample(sampleCpu.Opcodes, sampleCpu.Initial, trace);
        sut.Run();

        Utilities.Equal(sampleCpu.Final, sut);
        Utilities.Equal(sampleCpu.Cycles, trace);
    }

    public static IEnumerable<object[]> Opcode2CJsonFeeder()
    {
        /* Flags   */
        /*   0     */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "2c 53 b6", "initial": { "pc": 54843, "s": 86, "a": 95, "x": 59, "y": 209, "p": 168, "ram": [ [54843, 44], [54844, 83], [54845, 182], [46675, 158], [54846, 183]]}, "final": { "pc": 54846, "s": 86, "a": 95, "x": 59, "y": 209, "p": 168, "ram": [ [46675, 158], [54843, 44], [54844, 83], [54845, 182], [54846, 183]]}, "cycles": [ [54843, 44, "read"], [54844, 83, "read"], [54845, 182, "read"], [46675, 158, "read"]] }""") };
        /*   2 Z   */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "2c b5 07", "initial": { "pc": 47973, "s": 126, "a": 0, "x": 105, "y": 156, "p": 105, "ram": [ [47973, 44], [47974, 181], [47975, 7], [1973, 105], [47976, 244]]}, "final": { "pc": 47976, "s": 126, "a": 0, "x": 105, "y": 156, "p": 107, "ram": [ [1973, 105], [47973, 44], [47974, 181], [47975, 7], [47976, 244]]}, "cycles": [ [47973, 44, "read"], [47974, 181, "read"], [47975, 7, "read"], [1973, 105, "read"]] }""") };
        /*  64 V   */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "2c 00 bd", "initial": { "pc": 14692, "s": 118, "a": 251, "x": 145, "y": 129, "p": 232, "ram": [ [14692, 44], [14693, 0], [14694, 189], [48384, 175], [14695, 227]]}, "final": { "pc": 14695, "s": 118, "a": 251, "x": 145, "y": 129, "p": 168, "ram": [ [14692, 44], [14693, 0], [14694, 189], [14695, 227], [48384, 175]]}, "cycles": [ [14692, 44, "read"], [14693, 0, "read"], [14694, 189, "read"], [48384, 175, "read"]] }""") };
        /*  66 VZ  */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "2c db 00", "initial": { "pc": 3691, "s": 236, "a": 173, "x": 35, "y": 154, "p": 170, "ram": [ [3691, 44], [3692, 219], [3693, 0], [219, 221], [3694, 211]]}, "final": { "pc": 3694, "s": 236, "a": 173, "x": 35, "y": 154, "p": 232, "ram": [ [219, 221], [3691, 44], [3692, 219], [3693, 0], [3694, 211]]}, "cycles": [ [3691, 44, "read"], [3692, 219, "read"], [3693, 0, "read"], [219, 221, "read"]] }""") };
        /* 128 N   */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "2c 8c 19", "initial": { "pc": 29202, "s": 209, "a": 0, "x": 75, "y": 113, "p": 162, "ram": [ [29202, 44], [29203, 140], [29204, 25], [6540, 15], [29205, 206]]}, "final": { "pc": 29205, "s": 209, "a": 0, "x": 75, "y": 113, "p": 34, "ram": [ [6540, 15], [29202, 44], [29203, 140], [29204, 25], [29205, 206]]}, "cycles": [ [29202, 44, "read"], [29203, 140, "read"], [29204, 25, "read"], [6540, 15, "read"]] }""") };
        /* 130 NZ  */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "2c 83 00", "initial": { "pc": 21347, "s": 174, "a": 96, "x": 116, "y": 192, "p": 42, "ram": [ [21347, 44], [21348, 131], [21349, 0], [131, 189], [21350, 57]]}, "final": { "pc": 21350, "s": 174, "a": 96, "x": 116, "y": 192, "p": 168, "ram": [ [131, 189], [21347, 44], [21348, 131], [21349, 0], [21350, 57]]}, "cycles": [ [21347, 44, "read"], [21348, 131, "read"], [21349, 0, "read"], [131, 189, "read"]] }""") };
        /* 192 NV  */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "2c 9a 9c", "initial": { "pc": 62537, "s": 188, "a": 212, "x": 76, "y": 252, "p": 36, "ram": [ [62537, 44], [62538, 154], [62539, 156], [40090, 196], [62540, 138]]}, "final": { "pc": 62540, "s": 188, "a": 212, "x": 76, "y": 252, "p": 228, "ram": [ [40090, 196], [62537, 44], [62538, 154], [62539, 156], [62540, 138]]}, "cycles": [ [62537, 44, "read"], [62538, 154, "read"], [62539, 156, "read"], [40090, 196, "read"]] }""") };
        /* 194 NVZ */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "2c 91 00", "initial": { "pc": 20757, "s": 164, "a": 49, "x": 155, "y": 94, "p": 34, "ram": [ [20757, 44], [20758, 145], [20759, 0], [145, 223], [20760, 144]]}, "final": { "pc": 20760, "s": 164, "a": 49, "x": 155, "y": 94, "p": 224, "ram": [ [145, 223], [20757, 44], [20758, 145], [20759, 0], [20760, 144]]}, "cycles": [ [20757, 44, "read"], [20758, 145, "read"], [20759, 0, "read"], [145, 223, "read"]] }""") };
    }
}