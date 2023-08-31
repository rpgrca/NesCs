namespace NesCs.Logic.Cpu.Operations;

internal interface IOperation
{
    (int, byte) Execute(Cpu6502 cpu, byte value, int address);
}
