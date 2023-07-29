namespace NesCs.Logic.Cpu.Instructions;

public class LdaInZeroPageXModeOpcodeB5 : ZeroIndexedMode
{
    protected override byte ObtainValueForIndex(Cpu6502 cpu) =>
        cpu.ReadByteFromRegisterX();
}