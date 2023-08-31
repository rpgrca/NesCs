using NesCs.Logic.Cpu.Instructions;

namespace NesCs.Logic.Cpu;

internal class IllegalReadIgnoreOpcode5C : IllegalReadIgnoreOpcode1C
{
    public override byte Opcode => 0x5C;
}