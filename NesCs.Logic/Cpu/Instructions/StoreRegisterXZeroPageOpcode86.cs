namespace NesCs.Logic.Cpu.Instructions;

public class StoreRegisterXZeroPageOpcode86 : IInstruction
{
    public void Execute(Cpu6502 cpu)
    {
        cpu.ReadyForNextInstruction();
        var address = cpu.ReadByteFromProgram();

        cpu.ReadyForNextInstruction();
        cpu.WriteByteToMemory(address, cpu.ReadByteFromRegisterX());
    }
}