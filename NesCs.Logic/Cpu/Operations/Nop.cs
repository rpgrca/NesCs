namespace NesCs.Logic.Cpu.Operations;

public class Nop : IOperation
{
    (int, byte) IOperation.Execute(Cpu6502 cpu, byte value, int address) =>
        (address, value);
}