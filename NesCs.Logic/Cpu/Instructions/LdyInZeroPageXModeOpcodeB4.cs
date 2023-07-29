using NesCs.Logic.Cpu.Instructions.Modes;

namespace NesCs.Logic.Cpu.Instructions;

public class LdyInZeroPageXModeOpcodeB4 : ZeroIndexedMode
{
    protected override byte ObtainValueForIndex(Cpu6502 cpu) =>
        cpu.ReadByteFromRegisterX();

    protected override void StoreValueInFinalDestination(Cpu6502 cpu, byte value) =>
        cpu.SetValueIntoRegisterY(value);
}