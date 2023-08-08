namespace NesCs.Logic.Cpu.Operations;

public class Decrement : IOperation
{
    private readonly Func<Cpu6502, int, byte, byte> _getValue;
    private readonly Action<Cpu6502, int, byte> _setValue;

    public Decrement(Func<Cpu6502, int, byte, byte> getValue, Action<Cpu6502, int, byte> setValue)
    {
        _getValue = getValue;
        _setValue = setValue;
    }

    public void Execute(Cpu6502 cpu, byte value, int address)
    {
        var result = (byte)(_getValue(cpu, address, value) - 1);
        _setValue(cpu, address, result);

        cpu.SetZeroFlagBasedOn(result);
        cpu.SetNegativeFlagBasedOn(result);
    }
}