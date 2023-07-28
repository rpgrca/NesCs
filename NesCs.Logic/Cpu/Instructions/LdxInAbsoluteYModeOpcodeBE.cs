namespace NesCs.Logic.Cpu.Instructions;

public class LdxInAbsoluteYModeOpcodeBE : LoadInAbsoluteIndexedMode
{
    protected override byte ObtainValueForIndex(Cpu6502 cpu) =>
        cpu.ReadByteFromRegisterY();

    protected override void StoreValueInFinalDestination(Cpu6502 cpu, byte value) =>
        cpu.SetValueIntoRegisterX(value);
}