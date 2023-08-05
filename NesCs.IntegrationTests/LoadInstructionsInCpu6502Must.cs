using NesCs.Common.Tests;

namespace NesCs.IntegrationTests;

public class LoadInstructionsInCpu6502Must
{
	[Theory]
    [ProcessorFileTestData("a0")]
    [ProcessorFileTestData("a1")]
    [ProcessorFileTestData("a2")]
    [ProcessorFileTestData("a3")]
    [ProcessorFileTestData("a4")]
    [ProcessorFileTestData("a5")]
    [ProcessorFileTestData("a6")]
    [ProcessorFileTestData("a9")]
    [ProcessorFileTestData("ac")]
    [ProcessorFileTestData("ad")]
    [ProcessorFileTestData("ae")]
    [ProcessorFileTestData("b1")]
    [ProcessorFileTestData("b4")]
    [ProcessorFileTestData("b5")]
    [ProcessorFileTestData("b6")]
    [ProcessorFileTestData("b9")]
    [ProcessorFileTestData("bc")]
    [ProcessorFileTestData("bd")]
    [ProcessorFileTestData("be")]
    public void Execute10000LoadTestsPerOpcodeCorrectly1(SampleCpu sampleCpu)
    {
        var trace = new List<(int, byte, string)>();
		var sut = Utilities.CreateSubjectUnderTestFromSample(sampleCpu.Initial, trace);
        sut.Step();

        Utilities.Equal(sampleCpu.Final, sut);
        Utilities.Equal(sampleCpu.Cycles, trace);
    }
}