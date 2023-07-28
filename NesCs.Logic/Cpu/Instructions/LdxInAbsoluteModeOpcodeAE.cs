namespace NesCs.Logic.Cpu.Instructions;

public class LdxInAbsoluteModeOpcodeAE : LoadInAbsoluteMode
{
    protected override void StoreValueInFinalDestination(Cpu6502 cpu, byte value) =>
        cpu.SetValueIntoRegisterX(value);
}