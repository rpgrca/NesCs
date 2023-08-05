namespace NesCs.Logic.Cpu.Instructions;

public class IllegalReadIgnoreOpcode14 : IInstruction
{
    public void Execute(Cpu6502 cpu)
    {
        cpu.ReadyForNextInstruction();
        var address = cpu.ReadByteFromMemory(cpu.ReadByteFromProgramCounter());

        cpu.ReadyForNextInstruction();
        _ = cpu.ReadByteFromMemory(address);

        address += cpu.ReadByteFromRegisterX();
        _ = cpu.ReadByteFromMemory(address);
    }
}