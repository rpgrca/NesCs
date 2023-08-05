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
		var sut = Utilities.CreateSubjectUnderTestFromSample(sampleCpu.Initial, trace);
        sut.Run();

        Utilities.Equal(sampleCpu.Final, sut);
        Utilities.Equal(sampleCpu.Cycles, trace);
    }

    [Theory]
    [ProcessorFileTestData("e1")]
    [ProcessorFileTestData("e5")]
    [ProcessorFileTestData("ed")]
    [ProcessorFileTestData("f1")]
    [ProcessorFileTestData("f5")]
    [ProcessorFileTestData("f9")]
    [ProcessorFileTestData("fd")]
    public void Execute10000SubtractionTestsPerOpcodeCorrectly(SampleCpu sampleCpu)
    {
        var trace = new List<(int, byte, string)>();
		var sut = Utilities.CreateSubjectUnderTestFromSample(sampleCpu.Initial, trace);
        sut.Run();

        Utilities.Equal(sampleCpu.Final, sut);
        Utilities.Equal(sampleCpu.Cycles, trace);
    }

    [Theory]
    [ProcessorFileTestData("c0")]
    [ProcessorFileTestData("c1")]
    [ProcessorFileTestData("c4")]
    [ProcessorFileTestData("c5")]
    [ProcessorFileTestData("c9")]
    [ProcessorFileTestData("cc")]
    [ProcessorFileTestData("cd")]
    [ProcessorFileTestData("d1")]
    [ProcessorFileTestData("d5")]
    [ProcessorFileTestData("d9")]
    [ProcessorFileTestData("dd")]
    [ProcessorFileTestData("e0")]
    [ProcessorFileTestData("e4")]
    [ProcessorFileTestData("ec")]
    public void Execute10000CompareTestsPerOpcodeCorrectly(SampleCpu sampleCpu)
    {
        var trace = new List<(int, byte, string)>();
		var sut = Utilities.CreateSubjectUnderTestFromSample(sampleCpu.Initial, trace);
        sut.Run();

        Utilities.Equal(sampleCpu.Final, sut);
        Utilities.Equal(sampleCpu.Cycles, trace);
    }

    [Theory]
    [ProcessorFileTestData("88")]
    [ProcessorFileTestData("c6")]
    [ProcessorFileTestData("c8")]
    [ProcessorFileTestData("ca")]
    [ProcessorFileTestData("ce")]
    [ProcessorFileTestData("d6")]
    [ProcessorFileTestData("de")]
    [ProcessorFileTestData("e6")]
    [ProcessorFileTestData("e8")]
    [ProcessorFileTestData("ee")]
    [ProcessorFileTestData("f6")]
    [ProcessorFileTestData("fe")]
    public void Execute10000ChangeTestsPerOpcodeCorrectly(SampleCpu sampleCpu)
    {
        var trace = new List<(int, byte, string)>();
		var sut = Utilities.CreateSubjectUnderTestFromSample(sampleCpu.Initial, trace);
        sut.Run();

        Utilities.Equal(sampleCpu.Final, sut);
        Utilities.Equal(sampleCpu.Cycles, trace);
    }

    [Theory]
    [ProcessorFileTestData("06")]
    [ProcessorFileTestData("0a")]
    [ProcessorFileTestData("0e")]
    [ProcessorFileTestData("16")]
    [ProcessorFileTestData("1e")]
    public void Execute10000ShiftTestsPerOpcodeCorrectly(SampleCpu sampleCpu)
    {
        var trace = new List<(int, byte, string)>();
		var sut = Utilities.CreateSubjectUnderTestFromSample(sampleCpu.Initial, trace);
        sut.Run();

        Utilities.Equal(sampleCpu.Final, sut);
        Utilities.Equal(sampleCpu.Cycles, trace);
    }

    [Theory]
    [ProcessorFileTestData("c3")]
    [ProcessorFileTestData("c7")]
    [ProcessorFileTestData("cf")]
    [ProcessorFileTestData("d3")]
    [ProcessorFileTestData("d7")]
    [ProcessorFileTestData("db")]
    [ProcessorFileTestData("df")]
    [ProcessorFileTestData("e3")]
    [ProcessorFileTestData("e7")]
    [ProcessorFileTestData("ef")]
    [ProcessorFileTestData("f3")]
    [ProcessorFileTestData("f7")]
    [ProcessorFileTestData("fb")]
    [ProcessorFileTestData("ff")]
    public void Execute10000IllegalMathTestsPerOpcodeCorrectly(SampleCpu sampleCpu)
    {
        var trace = new List<(int, byte, string)>();
		var sut = Utilities.CreateSubjectUnderTestFromSample(sampleCpu.Initial, trace);
        sut.Run();

        Utilities.Equal(sampleCpu.Final, sut);
        Utilities.Equal(sampleCpu.Cycles, trace);
    }
}