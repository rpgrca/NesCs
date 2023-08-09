namespace NesCs.Logic.Cpu.Operations;

public class Load : IOperation
{
    private readonly Action<Cpu6502, byte> _loader;

    public Load(Action<Cpu6502, byte> loader) => _loader = loader;

    (int, byte) IOperation.Execute(Cpu6502 cpu, byte value, int address)
    {
        _loader(cpu, value);

        cpu.SetZeroFlagBasedOn(value);
        cpu.SetNegativeFlagBasedOn(value);

        return (address, value);
    }
}