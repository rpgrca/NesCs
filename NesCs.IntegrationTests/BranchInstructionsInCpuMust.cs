using NesCs.Common.Tests;

namespace NesCs.IntegrationTests;

public class BranchInstructionsInCpuMust
{
    [Theory]
    [ProcessorFileTestData("00")]
    [ProcessorFileTestData("10")]
    [ProcessorFileTestData("20")]
    [ProcessorFileTestData("30")]
    [ProcessorFileTestData("4c")]
    [ProcessorFileTestData("50")]
    [ProcessorFileTestData("6c")]
    [ProcessorFileTestData("70")]
    [ProcessorFileTestData("90")]
    [ProcessorFileTestData("b0")]
    [ProcessorFileTestData("d0")]
    [ProcessorFileTestData("f0")]
    public void Execute10000BranchTestsPerOpcodeCorrectly(SampleCpu sampleCpu)
    {
        var trace = new List<(int, byte, string)>();
		var sut = Utilities.CreateSubjectUnderTestFromSample(sampleCpu.Initial, trace);
        sut.Step();

        Utilities.Equal(sampleCpu.Final, sut);
        Utilities.Equal(sampleCpu.Cycles, trace);
    }
}