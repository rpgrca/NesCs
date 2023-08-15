namespace NesCs.Logic.Cpu.Instructions;

public class IllegalReadIgnoreOpcodeFC : IllegalReadIgnoreOpcode1C
{
    public override string Name => "IGN";

    public override byte Opcode => 0xFC;
}