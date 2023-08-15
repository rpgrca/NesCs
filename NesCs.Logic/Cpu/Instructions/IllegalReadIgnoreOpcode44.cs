namespace NesCs.Logic.Cpu.Instructions;

public class IllegalReadIgnoreOpcode44 : IllegalReadIgnoreOpcode04
{
    public override string Name => "IGN";

    public override byte Opcode => 0x7C;
}