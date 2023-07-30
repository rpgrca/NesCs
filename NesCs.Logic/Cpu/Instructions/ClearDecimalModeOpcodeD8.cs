namespace NesCs.Logic.Cpu.Instructions;

public class ClearDecimalModeOpcodeD8 : IInstruction
{
    public void Execute(Cpu6502 cpu)
    {
        cpu.ReadyForNextInstruction();
        _ = cpu.ReadByteFromMemory(cpu.ReadByteFromProgramCounter());

        cpu.ClearDecimalMode();
    }
}