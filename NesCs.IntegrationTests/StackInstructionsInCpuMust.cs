using NesCs.Common.Tests;

namespace NesCs.IntegrationTests;

public class StackInstructionsInCpuMust
{
    [Theory]
    [ProcessorFileTestData("08")]
    [ProcessorFileTestData("28")]
    [ProcessorFileTestData("40")]
    [ProcessorFileTestData("48")]
    [ProcessorFileTestData("60")]
    [ProcessorFileTestData("68")]
    public void Execute10000PullTestsPerOpcodeCorrectly(SampleCpu sampleCpu)
    {
        var trace = new List<(int, byte, string)>();
		var sut = Utilities.CreateSubjectUnderTestFromSample(sampleCpu.Initial, trace);
        sut.Run();

        Utilities.Equal(sampleCpu.Final, sut);
        Utilities.Equal(sampleCpu.Cycles, trace);
    }
}