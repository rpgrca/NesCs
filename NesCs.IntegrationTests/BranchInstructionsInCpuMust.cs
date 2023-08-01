using NesCs.Common.Tests;

namespace NesCs.IntegrationTests;

public class BranchInstructionsInCpuMust
{
    [Theory]
    [ProcessorFileTestData("10")]
    [ProcessorFileTestData("90")]
    [ProcessorFileTestData("d0")]
    [ProcessorFileTestData("f0")]
    public void Execute10000BranchTestsPerOpcodeCorrectly(SampleCpu sampleCpu)
    {
        var trace = new List<(int, byte, string)>();
		var sut = Utilities.CreateSubjectUnderTestFromSample(sampleCpu.Opcodes, sampleCpu.Initial, trace);
        sut.Run();

        Utilities.Equal(sampleCpu.Final, sut);
        Utilities.Equal(sampleCpu.Cycles, trace);
    }
}