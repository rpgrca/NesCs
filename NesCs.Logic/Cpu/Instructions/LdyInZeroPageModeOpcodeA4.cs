namespace NesCs.Logic.Cpu.Instructions;

public class LdyInZeroPageModeOpcodeA4 : LoadInZeroPageMode
{
    protected override void StoreValueInFinalDestination(Cpu6502 cpu, byte value) =>
        cpu.SetValueIntoRegisterY(value);
}