using NesCs.Common.Tests;

namespace NesCs.IntegrationTests;

public class InclusiveOrInstructionsInCpu6502Must
{
    [Theory]
    [ProcessorFileTestData("01")]
    [ProcessorFileTestData("05")]
    [ProcessorFileTestData("09")]
    [ProcessorFileTestData("0d")]
    [ProcessorFileTestData("11")]
    [ProcessorFileTestData("15")]
    [ProcessorFileTestData("19")]
    [ProcessorFileTestData("1d")]
    public void Execute10000InclusiveOrTestsPerOpcodeCorrectly(SampleCpu sampleCpu)
    {
        var trace = new List<(int, byte, string)>();
		var sut = Utilities.CreateSubjectUnderTestFromSample(sampleCpu.Opcodes, sampleCpu.Initial, trace);
        sut.Run();

        Utilities.Equal(sampleCpu.Final, sut);
        Utilities.Equal(sampleCpu.Cycles, trace);
    }
}
