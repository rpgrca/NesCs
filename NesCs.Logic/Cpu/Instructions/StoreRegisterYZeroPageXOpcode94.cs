namespace NesCs.Logic.Cpu.Instructions;

public class StoreRegisterYZeroPageXOpcode94 : IInstruction
{
    public void Execute(Cpu6502 cpu)
    {
        cpu.ReadyForNextInstruction();
        var address = cpu.ReadByteFromProgram();
        _ = cpu.ReadByteFromMemory(address);

        cpu.ReadyForNextInstruction();
        address = (byte)(address + cpu.ReadByteFromRegisterX());
        cpu.WriteByteToMemory(address, cpu.ReadByteFromRegisterY());
    }
}