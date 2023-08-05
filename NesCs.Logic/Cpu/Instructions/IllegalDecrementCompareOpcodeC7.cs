namespace NesCs.Logic.Cpu.Instructions;

public class IllegalDecrementCompareOpcodeC7 : IInstruction
{
    public void Execute(Cpu6502 cpu)
    {
        cpu.ReadyForNextInstruction();
        var address = cpu.ReadByteFromProgram();
        var value = cpu.ReadByteFromMemory(address);

        cpu.ReadyForNextInstruction();
        cpu.WriteByteToMemory(address, value);

        value = (byte)(value - 1);
        cpu.WriteByteToMemory(address, value);

        var result = (ProcessorStatus)(cpu.ReadByteFromAccumulator() - value);
        cpu.ClearCarryFlag();
        cpu.ClearNegativeFlag();
        cpu.ClearZeroFlag();
        if (cpu.ReadByteFromAccumulator() >= value)
        {
            cpu.SetCarryFlag();
            if (cpu.ReadByteFromAccumulator() == value)
            {
                cpu.SetZeroFlag();
            }
        }

        if ((result & ProcessorStatus.N) == ProcessorStatus.N)
        {
            cpu.SetNegativeFlag();
        }

    }
}