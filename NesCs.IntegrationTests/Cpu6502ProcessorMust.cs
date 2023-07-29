using NesCs.Tests.Common;

namespace NesCs.IntegrationTests;

public class Cpu6502ProcessorMust
{
	[Theory]
    [ProcessorFileTestData("a0")]
    [ProcessorFileTestData("a1")]
    [ProcessorFileTestData("a2")]
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
		var sut = Utilities.CreateSubjectUnderTestFromSample(sampleCpu.Opcodes, sampleCpu.Initial, trace);
        sut.Run();

        Utilities.Equal(sampleCpu.Final, sut);
        Utilities.Equal(sampleCpu.Cycles, trace);
    }

    [Theory]
    [ProcessorFileTestData("ea")]
    public void Execute10000NopTestsPerOpcodeCorrectly(SampleCpu sampleCpu)
    {
        var trace = new List<(int, byte, string)>();
		var sut = Utilities.CreateSubjectUnderTestFromSample(sampleCpu.Opcodes, sampleCpu.Initial, trace);
        sut.Run();

        Utilities.Equal(sampleCpu.Final, sut);
        Utilities.Equal(sampleCpu.Cycles, trace);
    }

    [Theory]
    [ProcessorFileTestData("01")]
    [ProcessorFileTestData("05")]
    [ProcessorFileTestData("09")]
    [ProcessorFileTestData("0d")]
    [ProcessorFileTestData("11")]
    [ProcessorFileTestData("15")]
    [ProcessorFileTestData("19")]
    [ProcessorFileTestData("1d")]
    public void Execute10000InclusiveOrTestsPerOpcodeCorrectly(SampleCpu sampleCpu)
    {
        var trace = new List<(int, byte, string)>();
		var sut = Utilities.CreateSubjectUnderTestFromSample(sampleCpu.Opcodes, sampleCpu.Initial, trace);
        sut.Run();

        Utilities.Equal(sampleCpu.Final, sut);
        Utilities.Equal(sampleCpu.Cycles, trace);
    }

    [Theory]
    [ProcessorFileTestData("25")]
    [ProcessorFileTestData("29")]
    [ProcessorFileTestData("2d")]
    [ProcessorFileTestData("35")]
    public void Execute10000LogicalAndTestsPerOpcodeCorrectly(SampleCpu sampleCpu)
    {
        var trace = new List<(int, byte, string)>();
		var sut = Utilities.CreateSubjectUnderTestFromSample(sampleCpu.Opcodes, sampleCpu.Initial, trace);
        sut.Run();

        Utilities.Equal(sampleCpu.Final, sut);
        Utilities.Equal(sampleCpu.Cycles, trace);
    }
}