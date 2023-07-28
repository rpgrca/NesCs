namespace NesCs.Logic.Cpu.Instructions;

public class LdyInZeroPageXModeOpcodeB4 : LoadInZeroIndexedMode
{
    protected override byte ObtainValueForIndex(Cpu6502 cpu) =>
        cpu.ReadByteFromRegisterX();

    protected override void StoreValueInFinalDestination(Cpu6502 cpu, byte value) =>
        cpu.SetValueIntoRegisterY(value);
}