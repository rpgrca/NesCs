namespace NesCs.Logic.Cpu.Instructions;

public class LdxInAbsoluteModeOpcodeAE : AbsoluteMode
{
    protected override void StoreValueInFinalDestination(Cpu6502 cpu, byte value) =>
        cpu.SetValueIntoRegisterX(value);
}