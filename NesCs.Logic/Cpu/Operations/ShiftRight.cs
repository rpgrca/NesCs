namespace NesCs.Logic.Cpu.Operations;

internal class ShiftRight : IOperation
{
    private readonly Action<Cpu6502, int, byte> _preemptiveWrite;
    private readonly Action<Cpu6502, int, byte> _setValue;

    public ShiftRight(Action<Cpu6502, int, byte> _preemptiveWrite, Action<Cpu6502, int, byte> setValue)
    {
        this._preemptiveWrite = _preemptiveWrite;
        _setValue = setValue;
    }

    (int, byte) IOperation.Execute(Cpu6502 cpu, byte value, int address)
    {
        _preemptiveWrite(cpu, address, value);
        if ((value & 1) == 1)
        {
            cpu.SetCarryFlag();
        }
        else
        {
            cpu.ClearCarryFlag();
        }

        value >>= 1;
       _setValue(cpu, address, value);

        cpu.SetNegativeFlagBasedOn(value);
        cpu.SetZeroFlagBasedOn(value);

        return (address, value);
    }
}