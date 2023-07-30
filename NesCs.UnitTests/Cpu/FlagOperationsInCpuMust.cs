using NesCs.Tests.Common;

namespace NesCs.UnitTests.Cpu;

public class FlagOperationsInCpuMust
{
    [Theory]
    [MemberData(nameof(Opcode18JsonFeeder))]
    [MemberData(nameof(Opcode58JsonFeeder))]
    [MemberData(nameof(OpcodeB8JsonFeeder))]
    [MemberData(nameof(OpcodeD8JsonFeeder))]
    public void BeExecutedCorrectly(SampleCpu sampleCpu)
    {
        var trace = new List<(int, byte, string)>();
        var sut = Utilities.CreateSubjectUnderTestFromSample(sampleCpu.Opcodes, sampleCpu.Initial, trace);
        sut.Run();

        Utilities.Equal(sampleCpu.Final, sut);
        Utilities.Equal(sampleCpu.Cycles, trace);
    }

    public static IEnumerable<object[]> Opcode18JsonFeeder()
    {
        /* No carry set */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "18 2d db", "initial": { "pc": 46370, "s": 169, "a": 138, "x": 75, "y": 143, "p": 224, "ram": [ [46370, 24], [46371, 45], [46372, 219]]}, "final": { "pc": 46371, "s": 169, "a": 138, "x": 75, "y": 143, "p": 224, "ram": [ [46370, 24], [46371, 45], [46372, 219]]}, "cycles": [ [46370, 24, "read"], [46371, 45, "read"]] }""") };
        /* Carry set    */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "18 bd 37", "initial": { "pc": 33057, "s": 162, "a": 126, "x": 230, "y": 151, "p": 39, "ram": [ [33057, 24], [33058, 189], [33059, 55]]}, "final": { "pc": 33058, "s": 162, "a": 126, "x": 230, "y": 151, "p": 38, "ram": [ [33057, 24], [33058, 189], [33059, 55]]}, "cycles": [ [33057, 24, "read"], [33058, 189, "read"]] }""")};
    }

    public static IEnumerable<object[]> Opcode58JsonFeeder()
    {
        /* No interrupt set */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "58 aa 12", "initial": { "pc": 12360, "s": 147, "a": 15, "x": 154, "y": 104, "p": 34, "ram": [ [12360, 88], [12361, 170], [12362, 18]]}, "final": { "pc": 12361, "s": 147, "a": 15, "x": 154, "y": 104, "p": 34, "ram": [ [12360, 88], [12361, 170], [12362, 18]]}, "cycles": [ [12360, 88, "read"], [12361, 170, "read"]] }""") };
        /* Interrupt set    */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "58 5b 95", "initial": { "pc": 22482, "s": 222, "a": 219, "x": 39, "y": 4, "p": 172, "ram": [ [22482, 88], [22483, 91], [22484, 149]]}, "final": { "pc": 22483, "s": 222, "a": 219, "x": 39, "y": 4, "p": 168, "ram": [ [22482, 88], [22483, 91], [22484, 149]]}, "cycles": [ [22482, 88, "read"], [22483, 91, "read"]] }""") };
    }

    public static IEnumerable<object[]> OpcodeB8JsonFeeder()
    {
        /* No overflow set */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "b8 38 9f", "initial": { "pc": 42118, "s": 59, "a": 175, "x": 249, "y": 243, "p": 160, "ram": [ [42118, 184], [42119, 56], [42120, 159]]}, "final": { "pc": 42119, "s": 59, "a": 175, "x": 249, "y": 243, "p": 160, "ram": [ [42118, 184], [42119, 56], [42120, 159]]}, "cycles": [ [42118, 184, "read"], [42119, 56, "read"]] }""") };
        /* Overflow set    */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "b8 a7 5d", "initial": { "pc": 32814, "s": 179, "a": 110, "x": 76, "y": 176, "p": 226, "ram": [ [32814, 184], [32815, 167], [32816, 93]]}, "final": { "pc": 32815, "s": 179, "a": 110, "x": 76, "y": 176, "p": 162, "ram": [ [32814, 184], [32815, 167], [32816, 93]]}, "cycles": [ [32814, 184, "read"], [32815, 167, "read"]] }""")};
    }

    public static IEnumerable<object[]> OpcodeD8JsonFeeder()
    {
        /* No decimal set */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "d8 01 b8", "initial": { "pc": 60103, "s": 196, "a": 110, "x": 41, "y": 251, "p": 37, "ram": [ [60103, 216], [60104, 1], [60105, 184]]}, "final": { "pc": 60104, "s": 196, "a": 110, "x": 41, "y": 251, "p": 37, "ram": [ [60103, 216], [60104, 1], [60105, 184]]}, "cycles": [ [60103, 216, "read"], [60104, 1, "read"]] }""") };
        /* Decimal set    */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "d8 45 dd", "initial": { "pc": 9012, "s": 248, "a": 154, "x": 207, "y": 181, "p": 41, "ram": [ [9012, 216], [9013, 69], [9014, 221]]}, "final": { "pc": 9013, "s": 248, "a": 154, "x": 207, "y": 181, "p": 33, "ram": [ [9012, 216], [9013, 69], [9014, 221]]}, "cycles": [ [9012, 216, "read"], [9013, 69, "read"]] }""")};
    }
}