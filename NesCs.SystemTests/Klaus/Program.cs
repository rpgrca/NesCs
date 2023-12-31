﻿using NesCs.Logic.Tracing;

public class Program
{
    public static void Main(string[] args)
    {
        //var bin = File.ReadAllBytes("../../6502_65C02_functional_tests/bin_files/6502_functional_test.bin");
        var bin = File.ReadAllBytes("../../../as65/6502_functional_test.bin");
        var cpu = new NesCs.Logic.Cpu.Cpu6502.Builder()
            .Running(bin)
            .ProgramMappedAt(0x0a)
            .WithSizeOf(65526)
            .WithProcessorStatusAs(NesCs.Logic.Cpu.ProcessorStatus.X)
            .WithProgramCounterAs(0x400)
            .TracingWith(new Vm6502DebuggerDisplay())
            .Build();

        cpu.Run();
    }
}