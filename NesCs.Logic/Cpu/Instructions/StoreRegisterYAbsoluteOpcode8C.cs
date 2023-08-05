namespace NesCs.Logic.Cpu.Instructions;

public class StoreRegisterYAbsoluteOpcode8C : IInstruction
{
    public void Execute(Cpu6502 cpu)
    {
        cpu.ReadyForNextInstruction();
        var low = cpu.ReadByteFromProgram();

        cpu.ReadyForNextInstruction();
        var high = cpu.ReadByteFromProgram();

        cpu.ReadyForNextInstruction();
        var address = high << 8 | low;
        cpu.WriteByteToMemory(address, cpu.ReadByteFromRegisterY());
    }
}