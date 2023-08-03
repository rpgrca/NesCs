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

        var pc = (ProcessorStatus)cpu.ReadByteFromStackMemory() & ~ProcessorStatus.B | ProcessorStatus.X;
        cpu.SetFlags(pc);
    }
}