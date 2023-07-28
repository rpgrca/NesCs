namespace NesCs.Logic.Cpu.Instructions;

public class OraInZeroPageXModeOpcode15 : IInstruction
{
    public void Execute(Cpu6502 cpu)
    {
        cpu.ReadyForNextInstruction();
        var offset = cpu.ReadByteFromProgram();

        cpu.ReadyForNextInstruction();
        _ = cpu.ReadByteFromMemory(offset);
        var effectiveAddress = (byte)((cpu.ReadByteFromRegisterX() + offset) & 0xff);

        var operand = cpu.ReadByteFromMemory(effectiveAddress);
        var result = (byte)(cpu.ReadByteFromAccumulator() | operand);
        cpu.SetValueIntoAccumulator(result);

        cpu.SetZeroFlagBasedOn(result);
        cpu.SetNegativeFlagBasedOn(result);
    }
}