namespace NesCs.Logic.Cpu.Instructions;

public class IllegalReadIgnoreOpcode7C : IllegalReadIgnoreOpcode1C
{
    public override string Name => "IGN";

    public override byte Opcode => 0x7C;
}