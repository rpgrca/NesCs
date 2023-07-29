namespace NesCs.Logic.Cpu.Instructions;

public class LdaInZeroPageModeOpcodeA5 : ZeroPageMode
{
    protected override byte ExecuteOperation(Cpu6502 cpu, byte value) => value;

    protected override void StoreValueInFinalDestination(Cpu6502 cpu, byte value) =>
        cpu.SetValueIntoAccumulator(value);
}