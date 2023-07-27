using System.Collections.Generic;
using System.Text.Json;
using NesCs.Logic.Cpu;
using NesCs.Tests.Common;

namespace NesCs.IntegrationTests;

public class Cpu6502ProcessorMust
{
	[Theory]
    [ClassData(typeof(OpcodeFeeder<OpcodeB5>))]
    public void Execute10000DifferentB5SampleTestsCorrectly(SampleCpuTest data)
    {
        var trace = new List<(int, byte, string)>();
        var sut = new Cpu6502.Builder()
            .RunningProgram(Convert.FromHexString(data.Name.Replace(" ", string.Empty)).Take(2).ToArray())
            .WithRamSizeOf(0xffff)
            .WithStackPointerAt(data.Initial.Value.S)
            .WithProcessorStatusAs(data.Initial.Value.P)
            .WithXAs(data.Initial.Value.X)
            .WithYAs(data.Initial.Value.Y)
            .WithAccumulatorAs(data.Initial.Value.A)
            .WithProgramCounterAs(data.Initial.Value.PC)
            .RamPatchedAs(data.Initial.Value.RAM)
            .TracingWith(trace)
            .Build();

        sut.Run();

        Utilities.Equal(data.Final, sut);
        Utilities.Equal(data.Cycles, trace);
    }
}

