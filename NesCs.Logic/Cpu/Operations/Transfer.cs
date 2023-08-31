namespace NesCs.Logic.Cpu.Operations;

internal class Transfer : IOperation
{
    private readonly Action<Cpu6502, int, byte> _storer;

    public Transfer(Action<Cpu6502, int, byte> storer) => _storer = storer;

    (int, byte) IOperation.Execute(Cpu6502 cpu, byte value, int address)
    {
        _storer(cpu, address, value);

        cpu.SetZeroFlagBasedOn(value);
        cpu.SetNegativeFlagBasedOn(value);

        return (address, value);
    }
}