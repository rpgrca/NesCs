using Xunit;
using System.Text.Json;
using NesCs.Logic.Cpu;

namespace NesCs.Tests.Common;

public static class Utilities
{
	public static Cpu6502 CreateSubjectUnderTestFromSample(SampleCpuTest data, List<(int, byte, string)> trace) =>
        new Cpu6502.Builder()
            .Running(Convert.FromHexString(data.Name.Replace(" ", string.Empty)).Take(2).ToArray())
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

	public static void Equal(SampleCpuTestStatus? final, Cpu6502 sut)
	{
		Assert.Equal(final.Value.S, sut.S);
		Assert.Equal(final.Value.X, sut.X);
		Assert.Equal(final.Value.Y, sut.Y);
		Assert.Equal(final.Value.A, sut.A);
		Assert.Equal(final.Value.P, (byte)sut.P);
		Assert.Equal(final.Value.PC, sut.PC);

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