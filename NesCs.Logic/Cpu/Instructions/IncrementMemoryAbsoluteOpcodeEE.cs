namespace NesCs.Logic.Cpu.Instructions;

public class IncrementMemoryAbsoluteOpcodeEE : IInstruction
{
    public void Execute(Cpu6502 cpu)
    {
        cpu.ReadyForNextInstruction();
        var low = cpu.ReadByteFromProgram();

        cpu.ReadyForNextInstruction();
        var high = cpu.ReadByteFromProgram();

        cpu.ReadyForNextInstruction();
        var address = high << 8 | low;
        var value = cpu.ReadByteFromMemory(address);

        cpu.WriteByteToMemory(address, value);
        value++;
        cpu.WriteByteToMemory(address, value);

        cpu.SetZeroFlagBasedOn(value);
        cpu.SetNegativeFlagBasedOn(value);
    }
}