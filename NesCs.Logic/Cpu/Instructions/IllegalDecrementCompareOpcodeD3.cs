namespace NesCs.Logic.Cpu.Instructions;

public class IllegalDecrementCompareOpcodeD3 : IInstruction
{
    public void Execute(Cpu6502 cpu)
    {
        cpu.ReadyForNextInstruction();
        var address = cpu.ReadByteFromProgram();
        var low = cpu.ReadByteFromMemory(address);

        address = (byte)(address + 1);
        cpu.ReadyForNextInstruction();
        var high = cpu.ReadByteFromMemory(address);

        var effectiveAddress = high << 8 | ((low + cpu.ReadByteFromRegisterY()) & 0xff);
        _ = cpu.ReadByteFromMemory(effectiveAddress);

        effectiveAddress = ((high << 8 | low) + cpu.ReadByteFromRegisterY()) & 0xffff;
        var value = cpu.ReadByteFromMemory(effectiveAddress);
        cpu.WriteByteToMemory(effectiveAddress, value);

        value = (byte)(value - 1);
        cpu.WriteByteToMemory(effectiveAddress, value);

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