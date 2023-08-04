namespace NesCs.Logic.Cpu.Instructions;

public class IllegalShiftLeftOrOpcode0F : IInstruction
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

        var operand = value << 1;
        if ((operand >> 8) != 0)
        {
            cpu.SetCarryFlag();
        }
        else
        {
            cpu.ClearCarryFlag();
        }

        var result = (byte)(operand & 0xff);
        cpu.WriteByteToMemory(address, result);

        result = (byte)(cpu.ReadByteFromAccumulator() | result);
        cpu.SetValueToAccumulator(result);
        cpu.SetNegativeFlagBasedOn(result);
        cpu.SetZeroFlagBasedOn(result);
    }
}