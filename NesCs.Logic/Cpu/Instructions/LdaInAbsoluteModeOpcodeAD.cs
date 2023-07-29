namespace NesCs.Logic.Cpu.Instructions;

public class LdaInAbsoluteModeOpcodeAD : AbsoluteMode
{
    protected override void StoreValueInFinalDestination(Cpu6502 cpu, byte value) =>
        cpu.SetValueIntoAccumulator(value);
}