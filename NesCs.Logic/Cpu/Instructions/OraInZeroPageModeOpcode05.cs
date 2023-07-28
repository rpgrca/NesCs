namespace NesCs.Logic.Cpu.Instructions;

public class OraInZeroPageModeOpcode05 : IInstruction
{
    public void Execute(Cpu6502 cpu)
    {
        cpu.ReadyForNextInstruction();
        var address = cpu.ReadByteFromProgram();

        cpu.ReadyForNextInstruction();
        var value = cpu.ReadByteFromMemory(address);

        value = (byte)(cpu.ReadByteFromAccumulator() | value);
        cpu.SetValueIntoAccumulator(value);

        cpu.SetZeroFlagBasedOn(value);
        cpu.SetNegativeFlagBasedOn(value);
    }
}