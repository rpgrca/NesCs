namespace NesCs.Logic.Cpu.Instructions;

internal class IllegalReadIgnoreOpcodeDC : IllegalReadIgnoreOpcode1C
{
    public override string Name => "IGN";

    public override byte Opcode => 0xDC;
}