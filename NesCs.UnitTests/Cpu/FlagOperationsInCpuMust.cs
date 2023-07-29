using NesCs.Tests.Common;

namespace NesCs.UnitTests.Cpu;

public class FlagOperationsInCpuMust
{
    [Theory]
    [MemberData(nameof(Opcode18JsonFeeder))]
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
}