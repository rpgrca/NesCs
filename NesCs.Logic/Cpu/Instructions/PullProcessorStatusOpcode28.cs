namespace NesCs.Logic.Cpu.Instructions;

public class PullProcessorStatusOpcode28 : IInstruction
{
    public void Execute(Cpu6502 cpu)
    {
        cpu.ReadyForNextInstruction();
        _ = cpu.ReadByteFromMemory(cpu.ReadByteFromProgramCounter());

        _ = cpu.ReadByteFromStackMemory();
        var sp = cpu.ReadByteFromStackPointer();
        sp += 1;
        cpu.SetValueIntoStackPointer(sp);

        var pc = (Cpu6502.ProcessorStatus)cpu.ReadByteFromStackMemory() & ~Cpu6502.ProcessorStatus.B | Cpu6502.ProcessorStatus.X;
        cpu.SetFlags(pc);
    }
}