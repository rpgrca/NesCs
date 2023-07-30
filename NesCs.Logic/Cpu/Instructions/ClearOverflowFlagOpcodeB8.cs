namespace NesCs.Logic.Cpu.Instructions;

public class ClearOverflowFlagOpcodeB8 : IInstruction
{
    public void Execute(Cpu6502 cpu)
    {
        cpu.ReadyForNextInstruction();
        _ = cpu.ReadByteFromMemory(cpu.ReadByteFromProgramCounter());

        cpu.ClearOverflowFlag();
    }
}