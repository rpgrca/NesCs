using NesCs.Common.Tests;

namespace NesCs.IntegrationTests;

public class TransferInstructionsInCpu6502Must
{
    [Theory]
    [ProcessorFileTestData("81")]
    [ProcessorFileTestData("84")]
    [ProcessorFileTestData("85")]
    [ProcessorFileTestData("86")]
    [ProcessorFileTestData("8a")]
    [ProcessorFileTestData("8c")]
    [ProcessorFileTestData("8d")]
    [ProcessorFileTestData("8e")]
    [ProcessorFileTestData("91")]
    [ProcessorFileTestData("94")]
    [ProcessorFileTestData("95")]
    [ProcessorFileTestData("96")]
    [ProcessorFileTestData("98")]
    [ProcessorFileTestData("99")]
    [ProcessorFileTestData("9a")]
    [ProcessorFileTestData("9d")]
    [ProcessorFileTestData("a8")]
    [ProcessorFileTestData("aa")]
    [ProcessorFileTestData("ba")]
    public void Execute10000TransferTestsPerOpcodeCorrectly(SampleCpu sampleCpu)
    {
        var trace = new List<(int, byte, string)>();
		var sut = Utilities.CreateSubjectUnderTestFromSample(sampleCpu.Initial, trace);
        sut.Run();

        Utilities.Equal(sampleCpu.Final, sut);
        Utilities.Equal(sampleCpu.Cycles, trace);
    }

    [ProcessorFileTestData("a7")]
    [ProcessorFileTestData("af")]
    [ProcessorFileTestData("b3")]
    public void Execute10000IllegalTransferTestsPerOpcodeCorrectly(SampleCpu sampleCpu)
    {
        var trace = new List<(int, byte, string)>();
		var sut = Utilities.CreateSubjectUnderTestFromSample(sampleCpu.Initial, trace);
        sut.Run();

        Utilities.Equal(sampleCpu.Final, sut);
        Utilities.Equal(sampleCpu.Cycles, trace);
    }
}