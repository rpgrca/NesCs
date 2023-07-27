namespace NesCs.Logic.Cpu.Instructions;

public class LdaInAbsoluteXModeOpcodeBD : IInstruction
{
    public void Execute(Cpu6502 cpu)
    {
        cpu.ReadyForNextInstruction();
        var low = cpu.ReadByteFromProgram();

        cpu.ReadyForNextInstruction();
        var high = cpu.ReadByteFromProgram();

        cpu.ReadyForNextInstruction();
        var address = ((high << 8) | low) + cpu.ReadByteFromRegisterX();
        var a = cpu.ReadByteFromMemory(address);
        cpu.SetValueIntoAccumulator(a);

        cpu.SetZeroFlagBasedOnAccumulator();
        cpu.SetNegativeFlagBasedOnAccumulator();
    }
}