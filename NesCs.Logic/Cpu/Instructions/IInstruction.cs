namespace NesCs.Logic.Cpu.Instructions;

public interface IInstruction : IDebuggerDisplay
{
    string Name { get; }
    byte Opcode { get; }
    byte[] PeekOperands(Cpu6502 cpu);
    void Execute(Cpu6502 cpu);
}