namespace NesCs.Logic.Cpu.Operations;

internal class Store : IOperation
{
    private readonly Action<Cpu6502, int, byte> _storer;

    public Store(Action<Cpu6502, int, byte> storer) => _storer = storer;

    (int, byte) IOperation.Execute(Cpu6502 cpu, byte value, int address)
    {
        _storer(cpu, address, value);
        return (address, value);
    }
}