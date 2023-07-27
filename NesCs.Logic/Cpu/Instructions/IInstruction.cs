namespace NesCs.Logic.Cpu.Instructions;

public interface IInstruction
{
    void Execute(Cpu6502 cpu);
}