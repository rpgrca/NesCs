namespace NesCs.Logic.Cpu.Instructions;

public class IllegalNopOpcode80 : IInstruction
{
    public void Execute(Cpu6502 cpu)
    {
        cpu.ReadyForNextInstruction();
        var pc = cpu.ReadByteFromProgramCounter();

        cpu.ReadyForNextInstruction();
        _ = cpu.ReadByteFromMemory(pc);
    }
}