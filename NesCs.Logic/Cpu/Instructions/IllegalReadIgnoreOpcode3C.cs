using NesCs.Logic.Cpu.Instructions;

namespace NesCs.Logic.Cpu;

internal class IllegalReadIgnoreOpcode3C : IllegalReadIgnoreOpcode1C
{
    public override string Name => "IGN";

    public override byte Opcode => 0x3C;
}