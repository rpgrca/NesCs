namespace NesCs.Logic.Cpu.Operations;

public interface IOperation
{
    void Execute(Cpu6502 cpu, byte value, int address);
}
