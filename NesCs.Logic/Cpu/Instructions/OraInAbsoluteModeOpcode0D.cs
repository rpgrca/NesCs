namespace NesCs.Logic.Cpu.Instructions;

public class OraInAbsoluteModeOpcode0D : IInstruction
{
    public void Execute(Cpu6502 cpu)
    {
        cpu.ReadyForNextInstruction();
        var low = cpu.ReadByteFromProgram();

        cpu.ReadyForNextInstruction();
        var high = cpu.ReadByteFromProgram();

        cpu.ReadyForNextInstruction();
        var value = cpu.ReadByteFromMemory(high << 8 | low);

        value = (byte)(cpu.ReadByteFromAccumulator() | value);
        cpu.SetValueIntoAccumulator(value);

        cpu.SetZeroFlagBasedOn(value);
        cpu.SetNegativeFlagBasedOn(value);
    }
}