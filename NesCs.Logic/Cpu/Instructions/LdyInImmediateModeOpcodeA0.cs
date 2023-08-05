using NesCs.Logic.Cpu.Instructions.Modes;

namespace NesCs.Logic.Cpu.Instructions;

public class LdyInImmediateModeOpcodeA0 : ImmediateMode
{
    protected override void StoreValueInFinalDestination(Cpu6502 cpu, byte value) =>
        cpu.SetValueToRegisterY(value);
}