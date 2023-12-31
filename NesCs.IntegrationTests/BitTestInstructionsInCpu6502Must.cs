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
    [ProcessorFileTestData("6e")]
    [ProcessorFileTestData("76")]
    [ProcessorFileTestData("7e")]
    public void Execute10000BitTestsPerOpcodeCorrectly(SampleCpu sampleCpu)
    {
        var trace = new List<(int, byte, string)>();
		var sut = Utilities.CreateSubjectUnderTestFromSample(sampleCpu.Initial, trace);
        sut.Step();

        Utilities.Equal(sampleCpu.Final, sut);
        Utilities.Equal(sampleCpu.Cycles, trace);
    }

    [Theory]
    [ProcessorFileTestData("03")]
    [ProcessorFileTestData("07")]
    [ProcessorFileTestData("0f")]
    [ProcessorFileTestData("13")]
    [ProcessorFileTestData("17")]
    [ProcessorFileTestData("1b")]
    [ProcessorFileTestData("1f")]
    [ProcessorFileTestData("23")]
    [ProcessorFileTestData("27")]
    [ProcessorFileTestData("2f")]
    [ProcessorFileTestData("33")]
    [ProcessorFileTestData("37")]
    [ProcessorFileTestData("3b")]
    [ProcessorFileTestData("3f")]
    [ProcessorFileTestData("43")]
    [ProcessorFileTestData("47")]
    [ProcessorFileTestData("4f")]
    [ProcessorFileTestData("53")]
    [ProcessorFileTestData("57")]
    [ProcessorFileTestData("5b")]
    [ProcessorFileTestData("5f")]
    [ProcessorFileTestData("63")]
    [ProcessorFileTestData("67")]
    [ProcessorFileTestData("6f")]
    [ProcessorFileTestData("73")]
    [ProcessorFileTestData("77")]
    [ProcessorFileTestData("7b")]
    [ProcessorFileTestData("7f")]
    public void Execute10000IllegalShiftTestsPerOpcodeCorrectly(SampleCpu sampleCpu)
    {
        var trace = new List<(int, byte, string)>();
		var sut = Utilities.CreateSubjectUnderTestFromSample(sampleCpu.Initial, trace);
        sut.Step();

        Utilities.Equal(sampleCpu.Final, sut);
        Utilities.Equal(sampleCpu.Cycles, trace);
    }
}