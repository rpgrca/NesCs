namespace NesCs.Logic.Cpu.Instructions;

public class LdxInZeroPageYModeOpcodeB6 : LoadInZeroIndexedMode
{
    protected override byte ObtainValueForIndex(Cpu6502 cpu) =>
        cpu.ReadByteFromRegisterY();

    protected override void StoreValueInFinalDestination(Cpu6502 cpu, byte value) =>
        cpu.SetValueIntoRegisterX(value);
}