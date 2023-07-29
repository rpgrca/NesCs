using NesCs.Tests.Common;

namespace NesCs.IntegrationTests;

public class TransferInstructionsInCpu6502Must
{
    [Theory]
    [ProcessorFileTestData("8a")]
    [ProcessorFileTestData("9a")]
    [ProcessorFileTestData("98")]
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
