namespace NesCs.Logic.Cpu.Operations;

internal class Compare : IOperation
{
    private readonly Func<Cpu6502, byte> _reader;

    public Compare(Func<Cpu6502, byte> reader) => _reader = reader;

    (int, byte) IOperation.Execute(Cpu6502 cpu, byte value, int address)
    {
        var minuend = _reader(cpu);
        cpu.ClearCarryFlag();
        cpu.ClearNegativeFlag();
        cpu.ClearZeroFlag();

        var result = (ProcessorStatus)(minuend - value);
        if (result >= 0)
        {
            cpu.SetCarryFlag();
            if (result == 0)
            {
                cpu.SetZeroFlag();
            }
        }

        if (result.HasFlag(ProcessorStatus.N))
        {
            cpu.SetNegativeFlag();
        }

        return (address, value);
    }
}