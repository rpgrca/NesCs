namespace NesCs.Logic.Cpu.Operations;

public interface IOperation
{
    (int, byte) Execute(Cpu6502 cpu, byte value, int address);
}
