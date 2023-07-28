namespace NesCs.Logic.Cpu.Instructions;

public class LdyInAbsoluteModeOpcodeAC : LoadInAbsoluteMode
{
    protected override void StoreValueInFinalDestination(Cpu6502 cpu, byte value) =>
        cpu.SetValueIntoRegisterY(value);
}