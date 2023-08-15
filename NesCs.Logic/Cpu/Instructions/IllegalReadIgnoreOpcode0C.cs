namespace NesCs.Logic.Cpu.Instructions;

public class IllegalReadIgnoreOpcode0C : IInstruction
{
    public string Name => "IGN";

    public byte Opcode => 0x0C;

    public void Execute(Cpu6502 cpu)
    {
        cpu.ReadyForNextInstruction();
        _ = cpu.ReadByteFromMemory(cpu.ReadByteFromProgramCounter());

        cpu.ReadyForNextInstruction();
        _ = cpu.ReadByteFromMemory(cpu.ReadByteFromProgramCounter());

#if NESDEV
        // TODO: Breaks Tom tests but fits golden log
        _ = cpu.ReadByteFromMemory(cpu.ReadByteFromProgramCounter());
#endif

        cpu.ReadyForNextInstruction();
    }
}