namespace NesCs.Logic.Cpu.Instructions;

public class StoreRegisterXZeroPageYOpcode96 : IInstruction
{
    public void Execute(Cpu6502 cpu)
    {
        cpu.ReadyForNextInstruction();
        var address = cpu.ReadByteFromProgram();
        _ = cpu.ReadByteFromMemory(address);

        cpu.ReadyForNextInstruction();
        address = (byte)(address + cpu.ReadByteFromRegisterY());
        cpu.WriteByteToMemory(address, cpu.ReadByteFromRegisterX());
    }
}