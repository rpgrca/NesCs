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
        sut.Step();

        Utilities.Equal(sampleCpu.Final, sut);
        Utilities.Equal(sampleCpu.Cycles, trace);
    }

    [Theory]
    [ProcessorFileTestData("83")]
    [ProcessorFileTestData("87")]
    [ProcessorFileTestData("8f")]
    [ProcessorFileTestData("97")]
    [ProcessorFileTestData("a7")]
    [ProcessorFileTestData("af")]
    [ProcessorFileTestData("b3")]
    [ProcessorFileTestData("b7")]
    [ProcessorFileTestData("bf")]
    public void Execute10000IllegalTransferTestsPerOpcodeCorrectly(SampleCpu sampleCpu)
    {
        var trace = new List<(int, byte, string)>();
		var sut = Utilities.CreateSubjectUnderTestFromSample(sampleCpu.Initial, trace);
        sut.Step();

        Utilities.Equal(sampleCpu.Final, sut);
        Utilities.Equal(sampleCpu.Cycles, trace);
    }
}