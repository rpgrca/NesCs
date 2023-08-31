namespace NesCs.Logic.Cpu.Operations;

internal class ShiftLeft : IOperation
{
    private readonly Action<Cpu6502, int, byte> _preemptiveWrite;
    private readonly Action<Cpu6502, int, byte> _setValue;

    public ShiftLeft(Action<Cpu6502, int, byte> _preemptiveWrite, Action<Cpu6502, int, byte> setValue)
    {
        this._preemptiveWrite = _preemptiveWrite;
        _setValue = setValue;
    }

    (int, byte) IOperation.Execute(Cpu6502 cpu, byte value, int address)
    {
        _preemptiveWrite(cpu, address, value);
        int result = value << 1;

        if ((result >> 8) != 0)
        {
            cpu.SetCarryFlag();
        }
        else
        {
            cpu.ClearCarryFlag();
        }
 
        value = (byte)(result & 0xff);
        _setValue(cpu, address, value);

        cpu.SetNegativeFlagBasedOn(value);
        cpu.SetZeroFlagBasedOn(value);

        return (address, value);
    }
}