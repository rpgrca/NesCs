namespace NesCs.Logic.Cpu.Instructions;

public class IllegalReadIgnoreOpcode04 : IInstruction
{
    public virtual string Name => "IGN";

    public virtual byte Opcode => 0x04;

    public void Execute(Cpu6502 cpu)
    {
        cpu.ReadyForNextInstruction();
        var address = cpu.ReadByteFromMemory(cpu.ReadByteFromProgramCounter());

        cpu.ReadyForNextInstruction();
        _ = cpu.ReadByteFromMemory(address);
    }
}