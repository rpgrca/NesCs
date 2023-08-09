namespace NesCs.Logic.Cpu.Operations;

public class Autocrement : IOperation
{
    private readonly Func<Cpu6502, int, byte, byte> _getValue;
    private readonly Action<Cpu6502, int, byte> _setValue;
    private readonly Func<byte, byte> _operation;

    public Autocrement(Func<Cpu6502, int, byte, byte> getValue, Action<Cpu6502, int, byte> setValue, Func<byte, byte> operation)
    {
        _getValue = getValue;
        _setValue = setValue;
        _operation = operation;
    }

    (int, byte) IOperation.Execute(Cpu6502 cpu, byte value, int address)
    {
        var result = _operation(_getValue(cpu, address, value));
        _setValue(cpu, address, result);

        cpu.SetZeroFlagBasedOn(result);
        cpu.SetNegativeFlagBasedOn(result);

        return (address, result);
    }
}