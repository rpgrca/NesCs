namespace NesCs.Logic.Cpu.Instructions;

public class IllegalRotateLeftAndOpcode3B : IInstruction
{
    public void Execute(Cpu6502 cpu)
    {
        cpu.ReadyForNextInstruction();
        var low = cpu.ReadByteFromProgram();

        cpu.ReadyForNextInstruction();
        var high = cpu.ReadByteFromProgram();

        var address = high << 8 | ((low + cpu.ReadByteFromRegisterY()) & 0xff);
        _ = cpu.ReadByteFromMemory(address);

        address = ((high << 8 | low) + cpu.ReadByteFromRegisterY()) & 0xffff;
        cpu.ReadyForNextInstruction();

        var value = cpu.ReadByteFromMemory(address);
        cpu.WriteByteToMemory(address, value);

        int rotatedValue = (value << 1) | (cpu.IsReadCarryFlagSet()? 1 : 0);
        if ((rotatedValue >> 8) != 0)
        {
            cpu.SetCarryFlag();
        }
        else
        {
            cpu.ClearCarryFlag();
        }

        var result = (byte)(rotatedValue & 0xff);
        cpu.WriteByteToMemory(address, result);

        result &= cpu.ReadByteFromAccumulator();
        cpu.SetValueToAccumulator(result);
        cpu.SetZeroFlagBasedOn(result);
        cpu.SetNegativeFlagBasedOn(result);
    }
}