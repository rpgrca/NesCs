namespace NesCs.Logic.Cpu.Instructions;

internal class IllegalReadIgnoreOpcode64 : IllegalReadIgnoreOpcode04
{
    public override string Name => "IGN";

    public override byte Opcode => 0x64;
}