using NesCs.Common.Tests;

namespace NesCs.UnitTests.Cpu;

public class OpcodesMust
{
    [Theory]
    [MemberData(nameof(OpcodeJsonFeeder))]
    public void ReturnNonEmptyNameAndCorrectOpcode(SampleCpu sampleCpu)
    {
        var tracer = new DisplayTracerSpy();
        var sut = Utilities.CreateSubjectUnderTestFromSampleForOpcodes(sampleCpu.Initial, tracer);
        sut.Step();

        Assert.NotEmpty(tracer.Name);
        Assert.Equal(sampleCpu.Opcode, tracer.Opcode);
    }

    public static IEnumerable<object[]> OpcodeJsonFeeder()
    {
        yield return new object[] { JsonDeserializer.Deserialize("""{ "name": "00 35 26", "initial": { "pc": 59521, "s": 242, "a": 4, "x": 71, "y": 56, "p": 97, "ram": [ [59521, 0], [59522, 53], [59523, 38], [65534, 21], [65535, 35], [8981, 229]]}, "final": { "pc": 8981, "s": 239, "a": 4, "x": 71, "y": 56, "p": 101, "ram": [ [496, 113], [497, 131], [498, 232], [8981, 229], [59521, 0], [59522, 53], [59523, 38], [65534, 21], [65535, 35]]}, "cycles": [ [59521, 0, "read"], [59522, 53, "read"], [498, 232, "write"], [497, 131, "write"], [496, 113, "write"], [65534, 21, "read"], [65535, 35, "read"]] }""") };
    }
}