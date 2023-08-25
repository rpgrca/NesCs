namespace NesCs.Logic.Cpu.Operations;

public class RotateLeft : IOperation
{
    private readonly Action<Cpu6502, int, byte> _extraWrite;
    private readonly Action<Cpu6502, int, byte> _setValue;

    public RotateLeft(Action<Cpu6502, int, byte> extraWrite, Action<Cpu6502, int, byte> setValue)
    {
        _extraWrite = extraWrite;
        _setValue = setValue;
    }

    (int, byte) IOperation.Execute(Cpu6502 cpu, byte value, int address)
    {
        _extraWrite(cpu, address, value);
        int rotatedValue = (value << 1) | (cpu.IsCarryFlagSet()? 1 : 0);

        if ((rotatedValue >> 8) != 0)
        {
            cpu.SetCarryFlag();
        }
        else
        {
            cpu.ClearCarryFlag();
        }

        var result = (byte)(rotatedValue & 0xff);
        _setValue(cpu, address, result);

        cpu.SetZeroFlagBasedOn(result);
        cpu.SetNegativeFlagBasedOn(result);

        return (address, result);
    }
}