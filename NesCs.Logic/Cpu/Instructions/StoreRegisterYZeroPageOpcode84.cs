namespace NesCs.Logic.Cpu.Instructions;

public class StoreRegisterYZeroPageOpcode84 : IInstruction
{
    public void Execute(Cpu6502 cpu)
    {
        cpu.ReadyForNextInstruction();
        var address = cpu.ReadByteFromProgram();

        cpu.ReadyForNextInstruction();
        cpu.WriteByteToMemory(address, cpu.ReadByteFromRegisterY());
    }
}