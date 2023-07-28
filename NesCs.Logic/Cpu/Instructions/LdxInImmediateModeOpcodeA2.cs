namespace NesCs.Logic.Cpu.Instructions;

public class LdxInImmediateModeOpcodeA2 : LoadInImmediateMode
{
    protected override void StoreValueInFinalDestination(Cpu6502 cpu, byte value) =>
        cpu.SetValueIntoRegisterX(value);
}