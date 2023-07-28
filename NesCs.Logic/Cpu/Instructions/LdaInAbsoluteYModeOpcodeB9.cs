namespace NesCs.Logic.Cpu.Instructions;

public class LdaInAbsoluteYModeOpcodeB9 : LoadInAbsoluteIndexedMode
{
    protected override byte ObtainValueForIndex(Cpu6502 cpu) =>
        cpu.ReadByteFromRegisterY();

    protected override void StoreValueInFinalDestination(Cpu6502 cpu, byte value) =>
        cpu.SetValueIntoAccumulator(value);
}