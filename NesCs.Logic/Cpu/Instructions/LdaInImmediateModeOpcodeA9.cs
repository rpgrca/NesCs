namespace NesCs.Logic.Cpu.Instructions;

public class LdaInImmediateModeOpcodeA9 : LoadInImmediateMode
{
    protected override void StoreValueInFinalDestination(Cpu6502 cpu, byte value) =>
        cpu.SetValueIntoAccumulator(value);
}