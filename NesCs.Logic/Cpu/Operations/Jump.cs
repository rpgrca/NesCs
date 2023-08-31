namespace NesCs.Logic.Cpu.Operations;

internal class Jump : IOperation
{
    public (int, byte) Execute(Cpu6502 cpu, byte value, int address)
    {
        cpu.SetValueToProgramCounter(address);
        return (address, value);
    }
}