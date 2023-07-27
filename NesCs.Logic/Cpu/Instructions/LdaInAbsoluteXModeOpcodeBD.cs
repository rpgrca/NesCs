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
        var address = (high << 8) | (low + cpu.ReadByteFromRegisterX()) & 0xff;
        var a = cpu.ReadByteFromMemory(address);
        cpu.SetValueIntoAccumulator(a);

        var address2 = ((high << 8) | low) + cpu.ReadByteFromRegisterX();
        if (address != address2)
        {
            a = cpu.ReadByteFromMemory(address2);
            cpu.SetValueIntoAccumulator(a);
        }

        cpu.SetZeroFlagBasedOnAccumulator();
        cpu.SetNegativeFlagBasedOnAccumulator();
    }
}