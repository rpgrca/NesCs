using NesCs.Common.Tests;

namespace NesCs.IntegrationTests;

public class NopInstructionsInCpu6502Must
{
    [Theory]
    [ProcessorFileTestData("04")]
    [ProcessorFileTestData("0c")]
    [ProcessorFileTestData("14")]
    [ProcessorFileTestData("44")]
    [ProcessorFileTestData("64")]
    [ProcessorFileTestData("ea")]
    public void Execute10000NopTestsPerOpcodeCorrectly(SampleCpu sampleCpu)
    {
        var trace = new List<(int, byte, string)>();
		var sut = Utilities.CreateSubjectUnderTestFromSample(sampleCpu.Initial, trace);
        sut.Run();

        Utilities.Equal(sampleCpu.Final, sut);
        Utilities.Equal(sampleCpu.Cycles, trace);
    }
}
