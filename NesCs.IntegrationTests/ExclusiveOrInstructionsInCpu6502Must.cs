using NesCs.Common.Tests;

namespace NesCs.IntegrationTests;

public class ExclusiveOrInstructionsInCpu6502Must
{
    [Theory]
    [ProcessorFileTestData("45")]
    [ProcessorFileTestData("49")]
    [ProcessorFileTestData("55")]
    public void Execute10000ExclusiveOrTestsPerOpcodeCorrectly(SampleCpu sampleCpu)
    {
        var trace = new List<(int, byte, string)>();
		var sut = Utilities.CreateSubjectUnderTestFromSample(sampleCpu.Opcodes, sampleCpu.Initial, trace);
        sut.Run();

        Utilities.Equal(sampleCpu.Final, sut);
        Utilities.Equal(sampleCpu.Cycles, trace);
    }
}