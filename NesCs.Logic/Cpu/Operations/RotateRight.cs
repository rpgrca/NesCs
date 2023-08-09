namespace NesCs.Logic.Cpu.Operations;

public class RotateRight : IOperation
{
    private readonly Action<Cpu6502, int, byte> _extraWrite;
    private readonly Action<Cpu6502, int, byte> _setValue;

    public RotateRight(Action<Cpu6502, int, byte> extraWrite, Action<Cpu6502, int, byte> setValue)
    {
        _extraWrite = extraWrite;
        _setValue = setValue;
    }

    public void Execute(Cpu6502 cpu, byte value, int address)
    {
        _extraWrite(cpu, address, value);
        var newCarry = (value & 1) == 1;

        var rotatedValue = (byte)(value >> 1);
        if (cpu.IsReadCarryFlagSet())
        {
            rotatedValue |= 1 << 7;
        }

        if (newCarry)
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
    }
}