namespace NesCs.Logic.Cpu.Instructions;

public class LdaInAbsoluteModeOpcodeAD : LoadInAbsoluteMode
{
    protected override void StoreValueInFinalDestination(Cpu6502 cpu, byte value) =>
        cpu.SetValueIntoAccumulator(value);
}