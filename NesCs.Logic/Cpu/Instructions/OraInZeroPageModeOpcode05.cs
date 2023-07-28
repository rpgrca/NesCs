namespace NesCs.Logic.Cpu.Instructions;

public class OraInZeroPageModeOpcode05 : IInstruction
{
    public void Execute(Cpu6502 cpu)
    {
        cpu.ReadyForNextInstruction();
        var offset = cpu.ReadByteFromProgram();

        cpu.ReadyForNextInstruction();

        var effectiveAddress = offset;

        var operand = cpu.ReadByteFromMemory(effectiveAddress);
        var result = (byte)(cpu.ReadByteFromAccumulator() | operand);
        cpu.SetValueIntoAccumulator(result);

        cpu.SetZeroFlagBasedOn(result);
        cpu.SetNegativeFlagBasedOn(result);
    }
}