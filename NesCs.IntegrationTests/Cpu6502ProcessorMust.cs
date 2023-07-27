using System.Collections.Generic;
using System.Text.Json;
using NesCs.Logic.Cpu;
using NesCs.Tests.Common;

namespace NesCs.IntegrationTests;

public class Cpu6502ProcessorMust
{
	[Theory]
    [ClassData(typeof(OpcodeFeeder<OpcodeB1>))]
    public void Execute10000DifferentB1SampleTestsCorrectly(SampleCpuTest data)
    {
        var trace = new List<(int, byte, string)>();
		var sut = Utilities.CreateSubjectUnderTestFromSample(data, trace);
        sut.Run();

        Utilities.Equal(data.Final, sut);
        Utilities.Equal(data.Cycles, trace);
    }

	[Theory]
    [ClassData(typeof(OpcodeFeeder<OpcodeB5>))]
    public void Execute10000DifferentB5SampleTestsCorrectly(SampleCpuTest data)
    {
        var trace = new List<(int, byte, string)>();
		var sut = Utilities.CreateSubjectUnderTestFromSample(data, trace);
        sut.Run();

        Utilities.Equal(data.Final, sut);
        Utilities.Equal(data.Cycles, trace);
    }
}

