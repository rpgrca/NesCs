namespace NesCs.Logic.Cpu.Operations;

internal class And : IOperation
{
    private readonly Action<Cpu6502, int, byte> _saver;
    private readonly Action<Cpu6502, byte> _flagCheck;

    public And(Action<Cpu6502, int, byte> saver, Action<Cpu6502, byte> flagCheck)
    {
        _saver = saver;
        _flagCheck = flagCheck;
    }

    (int, byte) IOperation.Execute(Cpu6502 cpu, byte value, int address)
    {
        var result = (byte)(cpu.ReadByteFromAccumulator() & value);
        _saver(cpu, address, result);
        _flagCheck(cpu, result);

        return (address, result);
    }
}