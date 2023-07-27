using NesCs.Tests.Common;

namespace NesCs.IntegrationTests;

public class Cpu6502ProcessorMust
{
	[Theory]
    [ClassData(typeof(OpcodeFeeder<OpcodeA1>))]
    [ClassData(typeof(OpcodeFeeder<OpcodeA5>))]
    [ClassData(typeof(OpcodeFeeder<OpcodeA9>))]
    [ClassData(typeof(OpcodeFeeder<OpcodeAD>))]
    [ClassData(typeof(OpcodeFeeder<OpcodeB1>))]
    [ClassData(typeof(OpcodeFeeder<OpcodeB5>))]
    [ClassData(typeof(OpcodeFeeder<OpcodeB9>))]
    [ClassData(typeof(OpcodeFeeder<OpcodeBD>))]
    public void Execute10000DifferentSampleTestsCorrectly(SampleCpuTest data)
    {
        var trace = new List<(int, byte, string)>();
		var sut = Utilities.CreateSubjectUnderTestFromSample(data, trace);
        sut.Run();

        Utilities.Equal(data.Final, sut);
        Utilities.Equal(data.Cycles, trace);
    }
}