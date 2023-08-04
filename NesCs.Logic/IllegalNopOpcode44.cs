namespace NesCs.Logic.Cpu.Instructions;

public class IllegalNopOpcode44 : IInstruction
{
    public void Execute(Cpu6502 cpu)
    {
        cpu.ReadyForNextInstruction();
        var address = cpu.ReadByteFromMemory(cpu.ReadByteFromProgramCounter());

        cpu.ReadyForNextInstruction();
        _ = cpu.ReadByteFromMemory(address);
    }
}