using NesCs.Common.Tests;

namespace NesCs.IntegrationTests;

public class StackInstructionsInCpuMust
{
    [Theory]
    [ProcessorFileTestData("28")]
    [ProcessorFileTestData("40")]
    [ProcessorFileTestData("60")]
    public void Execute10000PullTestsPerOpcodeCorrectly(SampleCpu sampleCpu)
    {
        var trace = new List<(int, byte, string)>();
		var sut = Utilities.CreateSubjectUnderTestFromSample(sampleCpu.Opcodes, sampleCpu.Initial, trace);
        sut.Run();

        Utilities.Equal(sampleCpu.Final, sut);
        Utilities.Equal(sampleCpu.Cycles, trace);
    }
}