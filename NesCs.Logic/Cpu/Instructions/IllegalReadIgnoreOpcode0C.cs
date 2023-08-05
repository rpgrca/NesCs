namespace NesCs.Logic.Cpu.Instructions;

public class IllegalReadIgnoreOpcode0C : IInstruction
{
    public void Execute(Cpu6502 cpu)
    {
        cpu.ReadyForNextInstruction();
        _ = cpu.ReadByteFromMemory(cpu.ReadByteFromProgramCounter());

        cpu.ReadyForNextInstruction();
        _ = cpu.ReadByteFromMemory(cpu.ReadByteFromProgramCounter());

        cpu.ReadyForNextInstruction();
    }
}