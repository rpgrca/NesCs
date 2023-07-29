using NesCs.Tests.Common;

namespace NesCs.IntegrationTests;

public class Cpu6502ProcessorMust
{
	[Theory]
    [ProcessorFileTest1Data(typeof(OpcodeA0))]
    [ProcessorFileTest1Data(typeof(OpcodeA1))]
    [ProcessorFileTest1Data(typeof(OpcodeA2))]
    [ProcessorFileTest1Data(typeof(OpcodeA4))]
    [ProcessorFileTest1Data(typeof(OpcodeA5))]
    [ProcessorFileTest1Data(typeof(OpcodeA6))]
    [ProcessorFileTest1Data(typeof(OpcodeA9))]
    [ProcessorFileTest1Data(typeof(OpcodeAC))]
    [ProcessorFileTest1Data(typeof(OpcodeAD))]
    [ProcessorFileTest1Data(typeof(OpcodeAE))]
    [ProcessorFileTest1Data(typeof(OpcodeB1))]
    [ProcessorFileTest1Data(typeof(OpcodeB4))]
    [ProcessorFileTest1Data(typeof(OpcodeB5))]
    [ProcessorFileTest1Data(typeof(OpcodeB6))]
    [ProcessorFileTest1Data(typeof(OpcodeB9))]
    [ProcessorFileTest1Data(typeof(OpcodeBC))]
    [ProcessorFileTest1Data(typeof(OpcodeBD))]
    [ProcessorFileTest1Data(typeof(OpcodeBE))]
    public void Execute10000LoadTestsPerOpcodeCorrectly1(SampleCpu sampleCpu)
    {
        var trace = new List<(int, byte, string)>();
		var sut = Utilities.CreateSubjectUnderTestFromSample(sampleCpu.Opcodes, sampleCpu.Initial, trace);
        sut.Run();

        Utilities.Equal(sampleCpu.Final, sut);
        Utilities.Equal(sampleCpu.Cycles, trace);
    }

    [Theory]
    [ClassData(typeof(OpcodeFeeder<OpcodeEA>))]
    public void Execute10000NopTestsPerOpcodeCorrectly(SampleCpuTest data)
    {
        var trace = new List<(int, byte, string)>();
		var sut = Utilities.CreateSubjectUnderTestFromSample(data, trace);
        sut.Run();

        Utilities.Equal(data.Final, sut);
        Utilities.Equal(data.Cycles, trace);
    }

    [Theory]
    [ClassData(typeof(OpcodeFeeder<Opcode01>))]
    [ClassData(typeof(OpcodeFeeder<Opcode05>))]
    [ClassData(typeof(OpcodeFeeder<Opcode09>))]
    [ClassData(typeof(OpcodeFeeder<Opcode0D>))]
    [ClassData(typeof(OpcodeFeeder<Opcode11>))]
    [ClassData(typeof(OpcodeFeeder<Opcode15>))]
    [ClassData(typeof(OpcodeFeeder<Opcode19>))]
    [ClassData(typeof(OpcodeFeeder<Opcode1D>))]
    public void Execute10000InclusiveOrTestsPerOpcodeCorrectly(SampleCpuTest data)
    {
        var trace = new List<(int, byte, string)>();
		var sut = Utilities.CreateSubjectUnderTestFromSample(data, trace);
        sut.Run();

        Utilities.Equal(data.Final, sut);
        Utilities.Equal(data.Cycles, trace);
    }

    [Theory]
    [ClassData(typeof(OpcodeFeeder<Opcode29>))]
    public void Execute10000LogicalAndTestsPerOpcodeCorrectly(SampleCpuTest data)
    {
        var trace = new List<(int, byte, string)>();
		var sut = Utilities.CreateSubjectUnderTestFromSample(data, trace);
        sut.Run();

        Utilities.Equal(data.Final, sut);
        Utilities.Equal(data.Cycles, trace);
    }
}