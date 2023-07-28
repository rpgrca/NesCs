namespace NesCs.Logic.Cpu.Instructions;

public class OraInImmediateModeOpcode09 : IInstruction
{
    public void Execute(Cpu6502 cpu)
    {
        cpu.ReadyForNextInstruction();
        var value = cpu.ReadByteFromProgram();

        cpu.ReadyForNextInstruction();
        value = (byte)(cpu.ReadByteFromAccumulator() | value);
        cpu.SetValueIntoAccumulator(value);

        cpu.SetZeroFlagBasedOnAccumulator();
        cpu.SetNegativeFlagBasedOnAccumulator();
    }
}