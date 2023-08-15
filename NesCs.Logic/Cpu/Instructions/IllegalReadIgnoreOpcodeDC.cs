namespace NesCs.Logic.Cpu.Instructions;

public class IllegalReadIgnoreOpcodeDC : IllegalReadIgnoreOpcode1C
{
    public override string Name => "IGN";

    public override byte Opcode => 0xDC;
}