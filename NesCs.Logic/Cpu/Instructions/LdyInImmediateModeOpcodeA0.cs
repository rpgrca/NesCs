namespace NesCs.Logic.Cpu.Instructions;

public class LdyInImmediateModeOpcodeA0 : LoadInImmediateMode
{
    protected override void StoreValueInFinalDestination(Cpu6502 cpu, byte value) =>
        cpu.SetValueIntoRegisterY(value);
}