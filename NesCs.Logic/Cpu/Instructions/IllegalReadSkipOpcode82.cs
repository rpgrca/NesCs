namespace NesCs.Logic.Cpu.Instructions;

public class IllegalReadSkipOpcode82 : IInstruction
{
    public void Execute(Cpu6502 cpu)
    {
        cpu.ReadyForNextInstruction();
        var pc = cpu.ReadByteFromProgramCounter();

        cpu.ReadyForNextInstruction();
        _ = cpu.ReadByteFromMemory(pc);
    }
}