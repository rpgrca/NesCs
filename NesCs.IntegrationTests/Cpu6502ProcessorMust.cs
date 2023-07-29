using NesCs.Tests.Common;

namespace NesCs.IntegrationTests;

public class Cpu6502ProcessorMust
{
	[Theory]
    [ProcessorFileTestData(typeof(OpcodeA0))]
    [ProcessorFileTestData(typeof(OpcodeA1))]
    [ProcessorFileTestData(typeof(OpcodeA2))]
    [ProcessorFileTestData(typeof(OpcodeA4))]
    [ProcessorFileTestData(typeof(OpcodeA5))]
    [ProcessorFileTestData(typeof(OpcodeA6))]
    [ProcessorFileTestData(typeof(OpcodeA9))]
    [ProcessorFileTestData(typeof(OpcodeAC))]
    [ProcessorFileTestData(typeof(OpcodeAD))]
    [ProcessorFileTestData(typeof(OpcodeAE))]
    [ProcessorFileTestData(typeof(OpcodeB1))]
    [ProcessorFileTestData(typeof(OpcodeB4))]
    [ProcessorFileTestData(typeof(OpcodeB5))]
    [ProcessorFileTestData(typeof(OpcodeB6))]
    [ProcessorFileTestData(typeof(OpcodeB9))]
    [ProcessorFileTestData(typeof(OpcodeBC))]
    [ProcessorFileTestData(typeof(OpcodeBD))]
    [ProcessorFileTestData(typeof(OpcodeBE))]
    public void Execute10000LoadTestsPerOpcodeCorrectly1(SampleCpu sampleCpu)
    {
        var trace = new List<(int, byte, string)>();
		var sut = Utilities.CreateSubjectUnderTestFromSample(sampleCpu.Opcodes, sampleCpu.Initial, trace);
        sut.Run();

        Utilities.Equal(sampleCpu.Final, sut);
        Utilities.Equal(sampleCpu.Cycles, trace);
    }

    [Theory]
    [ProcessorFileTestData(typeof(OpcodeEA))]
    public void Execute10000NopTestsPerOpcodeCorrectly(SampleCpu sampleCpu)
    {
        var trace = new List<(int, byte, string)>();
		var sut = Utilities.CreateSubjectUnderTestFromSample(sampleCpu.Opcodes, sampleCpu.Initial, trace);
        sut.Run();

        Utilities.Equal(sampleCpu.Final, sut);
        Utilities.Equal(sampleCpu.Cycles, trace);
    }

    [Theory]
    [ProcessorFileTestData(typeof(Opcode01))]
    [ProcessorFileTestData(typeof(Opcode05))]
    [ProcessorFileTestData(typeof(Opcode09))]
    [ProcessorFileTestData(typeof(Opcode0D))]
    [ProcessorFileTestData(typeof(Opcode11))]
    [ProcessorFileTestData(typeof(Opcode15))]
    [ProcessorFileTestData(typeof(Opcode19))]
    [ProcessorFileTestData(typeof(Opcode1D))]
    public void Execute10000InclusiveOrTestsPerOpcodeCorrectly(SampleCpu sampleCpu)
    {
        var trace = new List<(int, byte, string)>();
		var sut = Utilities.CreateSubjectUnderTestFromSample(sampleCpu.Opcodes, sampleCpu.Initial, trace);
        sut.Run();

        Utilities.Equal(sampleCpu.Final, sut);
        Utilities.Equal(sampleCpu.Cycles, trace);
    }

    [Theory]
    [ProcessorFileTestData(typeof(Opcode29))]
    public void Execute10000LogicalAndTestsPerOpcodeCorrectly(SampleCpu sampleCpu)
    {
        var trace = new List<(int, byte, string)>();
		var sut = Utilities.CreateSubjectUnderTestFromSample(sampleCpu.Opcodes, sampleCpu.Initial, trace);
        sut.Run();

        Utilities.Equal(sampleCpu.Final, sut);
        Utilities.Equal(sampleCpu.Cycles, trace);
    }
}