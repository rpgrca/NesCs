namespace NesCs.Logic.Cpu.Instructions;

public class OraInAbsoluteXModeOpcode0D : IInstruction
{
    public void Execute(Cpu6502 cpu)
    {
        cpu.ReadyForNextInstruction();
        var low = cpu.ReadByteFromProgram();

        cpu.ReadyForNextInstruction();
        var high = cpu.ReadByteFromProgram();

        cpu.ReadyForNextInstruction();
        var address = (high << 8) | (low + cpu.ReadByteFromRegisterX()) & 0xff;
        var value = cpu.ReadByteFromMemory(address);

        var address2 = (((high << 8) | low) + cpu.ReadByteFromRegisterX()) & 0xffff;
        if (address != address2)
        {
            value = cpu.ReadByteFromMemory(address2);
        }

        value |= cpu.ReadByteFromAccumulator();
        cpu.SetValueIntoAccumulator(value);
        cpu.SetZeroFlagBasedOn(value);
        cpu.SetNegativeFlagBasedOn(value);
    }
}