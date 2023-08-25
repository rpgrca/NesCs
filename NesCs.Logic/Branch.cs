namespace NesCs.Logic.Cpu.Operations;

public class Branch : IOperation
{
    private readonly Func<Cpu6502, bool> _check;

    public Branch(Func<Cpu6502, bool> check) => _check = check;

    public (int, byte) Execute(Cpu6502 cpu, byte value, int address)
    {
        var newPc = 0;

        if (_check(cpu))
        {
            var pc = cpu.ReadByteFromProgramCounter();
            _ = cpu.ReadByteFromMemory(pc);

            newPc = (pc + address) & 0xffff;
            cpu.SetValueToProgramCounter(newPc);

            var v = (pc / 256) - (newPc / 256);
            if (v != 0)
            {
                _ = cpu.ReadByteFromMemory(newPc + (v * 256));
            }
        }

        return (newPc, 0);
    }
}