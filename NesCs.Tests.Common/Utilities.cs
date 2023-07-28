using Xunit;
using System.Text.Json;
using NesCs.Logic.Cpu;

namespace NesCs.Tests.Common;

public static class Utilities
{
	public static Cpu6502 CreateSubjectUnderTestFromSample(SampleCpuTest data, List<(int, byte, string)> trace) =>
        new Cpu6502.Builder()
            .Running(Convert.FromHexString(data.Name.Replace(" ", string.Empty)))
			.StartingAt(0)
			.EndingAt(1)
            .WithRamSizeOf(0x10000)
            .WithStackPointerAt(data.Initial.Value.S)
            .WithProcessorStatusAs(data.Initial.Value.P)
            .WithXAs(data.Initial.Value.X)
            .WithYAs(data.Initial.Value.Y)
			.WithAccumulatorAs(data.Initial.Value.A)
            .WithProgramCounterAs(data.Initial.Value.PC)
            .RamPatchedAs(data.Initial.Value.RAM.Select(p => ((int)p[0], (byte)p[1])).ToArray())
			.TracingWith(trace)
            .Build();

	public static Cpu6502 CreateSubjectUnderTestFromSample(byte[] opcodes, (byte S, byte P, int PC, byte X, byte Y, byte A, (int Address, byte Value)[] RAM) initial, List<(int, byte, string)> trace) =>
        new Cpu6502.Builder()
            .Running(opcodes)
			.StartingAt(0)
			.EndingAt(1)
            .WithRamSizeOf(0x10000)
            .WithStackPointerAt(initial.S)
            .WithProcessorStatusAs(initial.P)
            .WithXAs(initial.X)
            .WithYAs(initial.Y)
			.WithAccumulatorAs(initial.A)
            .WithProgramCounterAs(initial.PC)
            .RamPatchedAs(initial.RAM)
			.TracingWith(trace)
            .Build();

	public static void Equal(SampleCpuTestStatus? final, Cpu6502 sut)
	{
		var (p, a, pc, x, y, s) = sut.TakeSnapshot();
		Assert.Equal(final.Value.S, s);
		Assert.Equal(final.Value.X, x);
		Assert.Equal(final.Value.Y, y);
		Assert.Equal(final.Value.A, a);
		Assert.Equal(final.Value.P, (byte)p);
		Assert.Equal(final.Value.PC, pc);

		foreach (var memory in final.Value.RAM)
		{
			Assert.Equal(memory[1], sut.PeekMemory(memory[0]));
		}
	}

	public static void Equal(JsonElement[][] cycles, List<(int, byte, string)> trace)
	{
		Assert.Equal(cycles.Length, trace.Count);
		for (var index = 0; index < cycles.Length; index++)
		{
			Assert.Equal(cycles[index][0].GetInt32(), trace[index].Item1);
			Assert.Equal(cycles[index][1].GetByte(), trace[index].Item2);
			Assert.Equal(cycles[index][2].GetString(), trace[index].Item3);
		}
	}
}