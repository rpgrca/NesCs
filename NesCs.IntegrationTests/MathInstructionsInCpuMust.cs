using NesCs.Common.Tests;

namespace NesCs.IntegrationTests;

public class MathInstructionsInCpuMust
{
    [Theory]
    [ProcessorFileTestData("61")]
    [ProcessorFileTestData("65")]
    [ProcessorFileTestData("69")]
    [ProcessorFileTestData("6d")]
    [ProcessorFileTestData("71")]
    [ProcessorFileTestData("75")]
    [ProcessorFileTestData("79")]
    [ProcessorFileTestData("7d")]
    [ProcessorFileTestData("e9")]
    public void Execute10000MathTestsPerOpcodeCorrectly(SampleCpu sampleCpu)
    {
        var trace = new List<(int, byte, string)>();
		var sut = Utilities.CreateSubjectUnderTestFromSample(sampleCpu.Opcodes, sampleCpu.Initial, trace);
        sut.Run();

        Utilities.Equal(sampleCpu.Final, sut);
        Utilities.Equal(sampleCpu.Cycles, trace);
    }

    [Theory]
    [ProcessorFileTestData("ed")]
    [ProcessorFileTestData("fd")]
    public void Execute10000SubtractionTestsPerOpcodeCorrectly(SampleCpu sampleCpu)
    {
        var trace = new List<(int, byte, string)>();
		var sut = Utilities.CreateSubjectUnderTestFromSample(sampleCpu.Opcodes, sampleCpu.Initial, trace);
        sut.Run();

        Utilities.Equal(sampleCpu.Final, sut);
        Utilities.Equal(sampleCpu.Cycles, trace);
    }
}