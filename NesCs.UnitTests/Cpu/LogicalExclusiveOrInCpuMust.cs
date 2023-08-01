using NesCs.Common.Tests;

namespace NesCs.UnitTests.Cpu;

public class LogicalExclusiveOrInCpuMust
{
	[Theory]
    [MemberData(nameof(Opcode45JsonFeeder))]
    [MemberData(nameof(Opcode49JsonFeeder))]
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
}