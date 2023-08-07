using NesCs.Logic.Cpu;

namespace NesCs.Logic.Cpu.Addressings;

public interface IAddressing
{
    byte ObtainValue(Cpu6502 cpu) => 0;
}