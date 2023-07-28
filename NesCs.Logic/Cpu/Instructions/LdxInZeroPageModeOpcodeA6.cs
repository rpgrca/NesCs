namespace NesCs.Logic.Cpu.Instructions;

public class LdxInZeroPageModeOpcodeA6 : LoadInZeroPageMode
{
    protected override void StoreValueInFinalDestination(Cpu6502 cpu, byte value) =>
        cpu.SetValueIntoRegisterX(value);
}