namespace NesCs.Logic.Cpu.Instructions;

internal class IllegalReadIgnoreOpcodeFC : IllegalReadIgnoreOpcode1C
{
    public override byte Opcode => 0xFC;
}