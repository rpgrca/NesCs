using NesCs.Logic.Cpu.Instructions.Modes;

namespace NesCs.Logic.Cpu.Instructions;

public class LdxInAbsoluteYModeOpcodeBE : AbsoluteIndexedMode
{
    protected override byte ExecuteOperation(Cpu6502 cpu, byte value) => value;

    protected override byte ObtainValueForIndex(Cpu6502 cpu) =>
        cpu.ReadByteFromRegisterY();

    protected override void StoreValueInFinalDestination(Cpu6502 cpu, byte value) =>
        cpu.SetValueToRegisterX(value);
}