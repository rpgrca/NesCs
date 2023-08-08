namespace NesCs.Logic.Cpu.Operations;

public class RotateLeft : IOperation
{
    public void Execute(Cpu6502 cpu, byte value, int address)
    {
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

        cpu.SetZeroFlagBasedOn(result);
        cpu.SetNegativeFlagBasedOn(result);
    }
}