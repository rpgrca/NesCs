using NesCs.Common.Tests;

namespace NesCs.IntegrationTests;

public class TransferInstructionsInCpu6502Must
{
    [Theory]
    [ProcessorFileTestData("85")]
    [ProcessorFileTestData("8a")]
    [ProcessorFileTestData("8d")]
    [ProcessorFileTestData("95")]
    [ProcessorFileTestData("98")]
    [ProcessorFileTestData("9a")]
    [ProcessorFileTestData("9d")]
    [ProcessorFileTestData("a8")]
    [ProcessorFileTestData("aa")]
    [ProcessorFileTestData("ba")]
    public void Execute10000TransferTestsPerOpcodeCorrectly(SampleCpu sampleCpu)
    {
        var trace = new List<(int, byte, string)>();
		var sut = Utilities.CreateSubjectUnderTestFromSample(sampleCpu.Opcodes, sampleCpu.Initial, trace);
        sut.Run();

        Utilities.Equal(sampleCpu.Final, sut);
        Utilities.Equal(sampleCpu.Cycles, trace);
    }
}