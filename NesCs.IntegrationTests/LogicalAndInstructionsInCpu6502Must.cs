using NesCs.Common.Tests;

namespace NesCs.IntegrationTests;

public class LogicalAndInstructionsInCpu6502Must
{
    [Theory]
    [ProcessorFileTestData("21")]
    [ProcessorFileTestData("25")]
    [ProcessorFileTestData("29")]
    [ProcessorFileTestData("2d")]
    [ProcessorFileTestData("31")]
    [ProcessorFileTestData("35")]
    [ProcessorFileTestData("39")]
    [ProcessorFileTestData("3d")]
    public void Execute10000LogicalAndTestsPerOpcodeCorrectly(SampleCpu sampleCpu)
    {
        var trace = new List<(int, byte, string)>();
		var sut = Utilities.CreateSubjectUnderTestFromSample(sampleCpu.Initial, trace);
        sut.Run();

        Utilities.Equal(sampleCpu.Final, sut);
        Utilities.Equal(sampleCpu.Cycles, trace);
    }
}