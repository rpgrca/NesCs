using NesCs.Common.Tests;

namespace NesCs.UnitTests.Cpu;

public class LogicalExclusiveOrInCpuMust
{
	[Theory]
    [MemberData(nameof(Opcode49JsonFeeder))]
	public void BeExecutedCorrectly(SampleCpu sampleCpu)
	{
        var trace = new List<(int, byte, string)>();
        var sut = Utilities.CreateSubjectUnderTestFromSample(sampleCpu.Opcodes, sampleCpu.Initial, trace);
        sut.Run();

        Utilities.Equal(sampleCpu.Final, sut);
        Utilities.Equal(sampleCpu.Cycles, trace);
	}

    public static IEnumerable<object[]> Opcode49JsonFeeder()
    {
        /*   0    */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "49 d9 22", "initial": { "pc": 53800, "s": 242, "a": 33, "x": 249, "y": 116, "p": 173, "ram": [ [53800, 73], [53801, 217], [53802, 34]]}, "final": { "pc": 53802, "s": 242, "a": 248, "x": 249, "y": 116, "p": 173, "ram": [ [53800, 73], [53801, 217], [53802, 34]]}, "cycles": [ [53800, 73, "read"], [53801, 217, "read"]] }""") };
        /*   2 Z  */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "49 b4 12", "initial": { "pc": 38951, "s": 46, "a": 110, "x": 186, "y": 118, "p": 174, "ram": [ [38951, 73], [38952, 180], [38953, 18]]}, "final": { "pc": 38953, "s": 46, "a": 218, "x": 186, "y": 118, "p": 172, "ram": [ [38951, 73], [38952, 180], [38953, 18]]}, "cycles": [ [38951, 73, "read"], [38952, 180, "read"]] }""") };
        /* 128 N  */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "49 78 c9", "initial": { "pc": 8256, "s": 18, "a": 65, "x": 41, "y": 182, "p": 168, "ram": [ [8256, 73], [8257, 120], [8258, 201]]}, "final": { "pc": 8258, "s": 18, "a": 57, "x": 41, "y": 182, "p": 40, "ram": [ [8256, 73], [8257, 120], [8258, 201]]}, "cycles": [ [8256, 73, "read"], [8257, 120, "read"]] }""") };
        /* 130 NZ */ yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "49 e1 2f", "initial": { "pc": 50746, "s": 129, "a": 218, "x": 97, "y": 117, "p": 171, "ram": [ [50746, 73], [50747, 225], [50748, 47]]}, "final": { "pc": 50748, "s": 129, "a": 59, "x": 97, "y": 117, "p": 41, "ram": [ [50746, 73], [50747, 225], [50748, 47]]}, "cycles": [ [50746, 73, "read"], [50747, 225, "read"]] }""") };
    }
}