namespace NesCs.Logic.Cpu.Instructions;

internal class IllegalReadIgnoreOpcodeDC : IllegalReadIgnoreOpcode1C
{
    public override byte Opcode => 0xDC;
}