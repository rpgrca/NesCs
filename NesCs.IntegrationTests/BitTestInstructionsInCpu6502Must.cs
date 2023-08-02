using NesCs.Common.Tests;

namespace NesCs.IntegrationTests;

public class BitTestInstructionsInCpu6502Must
{
    [Theory]
    [ProcessorFileTestData("24")]
    [ProcessorFileTestData("26")]
    [ProcessorFileTestData("2a")]
    [ProcessorFileTestData("2c")]
    [ProcessorFileTestData("2e")]
    [ProcessorFileTestData("36")]
    [ProcessorFileTestData("3e")]
    [ProcessorFileTestData("46")]
    [ProcessorFileTestData("4a")]
    [ProcessorFileTestData("4e")]
    [ProcessorFileTestData("56")]
    [ProcessorFileTestData("5e")]
    [ProcessorFileTestData("66")]
    [ProcessorFileTestData("6a")]
    public void Execute10000BitTestsPerOpcodeCorrectly(SampleCpu sampleCpu)
    {
        var trace = new List<(int, byte, string)>();
		var sut = Utilities.CreateSubjectUnderTestFromSample(sampleCpu.Opcodes, sampleCpu.Initial, trace);
        sut.Run();

        Utilities.Equal(sampleCpu.Final, sut);
        Utilities.Equal(sampleCpu.Cycles, trace);
    }
}