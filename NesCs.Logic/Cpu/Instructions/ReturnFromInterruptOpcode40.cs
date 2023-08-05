namespace NesCs.Logic.Cpu.Instructions;

public class ReturnFromInterruptOpcode40 : IInstruction
{
    public void Execute(Cpu6502 cpu)
    {
        cpu.ReadyForNextInstruction();
        _ = cpu.ReadByteFromMemory(cpu.ReadByteFromProgramCounter());

        cpu.ReadyForNextInstruction();
        _ = cpu.ReadByteFromStackMemory();
        var sp = cpu.ReadByteFromStackPointer();
        sp += 1;
        cpu.SetValueToStackPointer(sp);

        cpu.ReadyForNextInstruction();
        var p = (ProcessorStatus)cpu.ReadByteFromStackMemory() & ~ProcessorStatus.B | ProcessorStatus.X;
        cpu.OverwriteFlags(p);
        cpu.ReadyForNextInstruction();
        sp += 1;

        cpu.ReadyForNextInstruction();
        cpu.SetValueToStackPointer(sp);
        var pcl = cpu.ReadByteFromStackMemory();

        sp += 1;
        cpu.SetValueToStackPointer(sp);

        var pch = cpu.ReadByteFromStackMemory();
        var address = pch << 8 | pcl;
        cpu.SetValueToProgramCounter(address);
    }
}