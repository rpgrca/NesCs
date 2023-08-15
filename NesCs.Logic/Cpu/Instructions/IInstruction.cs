namespace NesCs.Logic.Cpu.Instructions;

public interface IInstruction
{
    string Name { get; }
    byte Opcode { get; }
    void Execute(Cpu6502 cpu);
}