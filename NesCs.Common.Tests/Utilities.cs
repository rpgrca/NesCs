using Xunit;
using NesCs.Logic.Cpu;
using NesCs.Logic.Ram;
using NesCs.Logic.Tracing;

namespace NesCs.Common.Tests;

public static class Utilities
{
    public static Cpu6502 CreateSubjectUnderTestFromSample(SampleStatus initial, List<(int, byte, string)> trace)
    {
        var ramController = new RamController.Builder().WithoutMirroring().Build();
        var ppu = new DummyPpu();
        ramController.RegisterHook(ppu);

        var cpu = new Cpu6502.Builder()
            .ProgramMappedAt(initial.PC)
            .WithRamController(ramController)
            .WithStackPointerAt(initial.S)
            .WithProcessorStatusAs(initial.P)
            .WithXAs(initial.X)
            .WithYAs(initial.Y)
            .WithAccumulatorAs(initial.A)
            .WithProgramCounterAs(initial.PC)
            .RamPatchedAs(initial.RAM.Select(p => (p.Address, p.Value)).ToArray())
            .TracingWith(new TracerSpy(trace))
            .SupportingInvalidInstructions()
            .Build();

        return cpu;
    }

    public static Cpu6502 CreateSubjectUnderTestFromSampleForOpcodes(SampleStatus initial, ITracer tracer)
    {
        var ramController = new RamController.Builder().WithoutMirroring().Build();
        var ppu = new DummyPpu();
        ramController.RegisterHook(ppu);

        var cpu = new Cpu6502.Builder()
            .ProgramMappedAt(initial.PC)
            .WithRamController(ramController)
            .WithStackPointerAt(initial.S)
            .WithProcessorStatusAs(initial.P)
            .WithXAs(initial.X)
            .WithYAs(initial.Y)
            .WithAccumulatorAs(initial.A)
            .WithProgramCounterAs(initial.PC)
            .RamPatchedAs(initial.RAM.Select(p => (p.Address, p.Value)).ToArray())
            .TracingWith(tracer)
            .SupportingInvalidInstructions()
            .Build();

        return cpu;
    }

    public static void Equal(SampleStatus final, Cpu6502 sut)
    {
        var (p, a, pc, x, y, s, _) = sut.TakeSnapshot();
        Assert.Equal(final.S, s);
        Assert.Equal(final.X, x);
        Assert.Equal(final.Y, y);
        Assert.Equal(final.A, a);
        Assert.Equal(final.P, p);
        Assert.Equal(final.PC, pc);

        foreach (var memory in final.RAM)
        {
            Assert.Equal(memory.Value, sut.PeekMemory(memory.Address));
        }
    }

    public static void Equal(SampleCycle[] cycles, List<(int Address, byte Value, string Type)> trace)
    {
        Assert.Equal(cycles.Length, trace.Count);
        for (var index = 0; index < cycles.Length; index++)
        {
            Assert.Equal(cycles[index].Address, trace[index].Address);
            Assert.Equal(cycles[index].Value, trace[index].Value);
            Assert.Equal(cycles[index].Type, trace[index].Type);
        }
    }
}