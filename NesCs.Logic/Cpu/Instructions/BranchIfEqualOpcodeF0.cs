namespace NesCs.Logic.Cpu.Instructions;

public class BranchIfEqualOpcodeF0 : IInstruction
{
    public void Execute(Cpu6502 cpu)
    {
        cpu.ReadyForNextInstruction();
        var value = cpu.ReadByteFromProgramCounter();

        cpu.ReadyForNextInstruction();
        var offset = cpu.ReadByteFromMemory(value);
        if (cpu.ReadZeroFlag())
        {

        }
    }
}