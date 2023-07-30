using NesCs.Common.Tests;

namespace NesCs.IntegrationTests;

public class BitTestInstructionsInCpu6502Must
{
    [Theory]
    [ProcessorFileTestData("24")]
    [ProcessorFileTestData("2c")]
    public void Execute10000BitTestsPerOpcodeCorrectly(SampleCpu sampleCpu)
    {
        var trace = new List<(int, byte, string)>();
		var sut = Utilities.CreateSubjectUnderTestFromSample(sampleCpu.Opcodes, sampleCpu.Initial, trace);
        sut.Run();

        Utilities.Equal(sampleCpu.Final, sut);
        Utilities.Equal(sampleCpu.Cycles, trace);
    }
}