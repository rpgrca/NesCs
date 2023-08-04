using NesCs.Common.Tests;

namespace NesCs.IntegrationTests;

public class NopInstructionsInCpu6502Must
{
    [Theory]
    [ProcessorFileTestData("04")]
    [ProcessorFileTestData("0c")]
    [ProcessorFileTestData("14")]
    [ProcessorFileTestData("1a")]
    [ProcessorFileTestData("1c")]
    [ProcessorFileTestData("34")]
    [ProcessorFileTestData("3a")]
    [ProcessorFileTestData("3c")]
    [ProcessorFileTestData("44")]
    [ProcessorFileTestData("54")]
    [ProcessorFileTestData("5a")]
    [ProcessorFileTestData("64")]
    [ProcessorFileTestData("74")]
    [ProcessorFileTestData("7a")]
    [ProcessorFileTestData("80")]
    [ProcessorFileTestData("d4")]
    [ProcessorFileTestData("da")]
    [ProcessorFileTestData("ea")]
    [ProcessorFileTestData("f4")]
    [ProcessorFileTestData("fa")]
    public void Execute10000NopTestsPerOpcodeCorrectly(SampleCpu sampleCpu)
    {
        var trace = new List<(int, byte, string)>();
		var sut = Utilities.CreateSubjectUnderTestFromSample(sampleCpu.Initial, trace);
        sut.Run();

        Utilities.Equal(sampleCpu.Final, sut);
        Utilities.Equal(sampleCpu.Cycles, trace);
    }
}
