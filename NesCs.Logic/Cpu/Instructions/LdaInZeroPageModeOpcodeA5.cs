namespace NesCs.Logic.Cpu.Instructions;

public class LdaInZeroPageModeOpcodeA5 : LoadInZeroPageMode
{
    protected override void StoreValueInFinalDestination(Cpu6502 cpu, byte value) =>
        cpu.SetValueIntoAccumulator(value);
}