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
        cpu.SetValueIntoStackPointer(sp);

        cpu.ReadyForNextInstruction();
        var p = (Cpu6502.ProcessorStatus)cpu.ReadByteFromStackMemory() & ~Cpu6502.ProcessorStatus.B | Cpu6502.ProcessorStatus.X;
        cpu.SetFlags(p);
        cpu.ReadyForNextInstruction();
        sp += 1;

        cpu.ReadyForNextInstruction();
        cpu.SetValueIntoStackPointer(sp);
        var pcl = cpu.ReadByteFromStackMemory();

        sp += 1;
        cpu.SetValueIntoStackPointer(sp);

        var pch = cpu.ReadByteFromStackMemory();
        var address = pch << 8 | pcl;
        cpu.SetValueIntoProgramCounter(address);
    }
}