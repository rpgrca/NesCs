namespace NesCs.Logic.Cpu.Operations;

public class Flag : IOperation
{
    private readonly Action<Cpu6502> _action;

    public Flag(Action<Cpu6502> action) => _action = action;

    public void Execute(Cpu6502 cpu, byte value, int address) => _action(cpu);
}