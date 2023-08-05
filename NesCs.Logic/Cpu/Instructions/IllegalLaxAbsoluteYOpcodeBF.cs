namespace NesCs.Logic.Cpu.Instructions;

public class IllegalLaxAbsoluteYOpcodeBF : IInstruction
{
    public void Execute(Cpu6502 cpu)
    {
        cpu.ReadyForNextInstruction();
        var low = cpu.ReadByteFromProgram();

        cpu.ReadyForNextInstruction();
        var high = cpu.ReadByteFromProgram();

        var address = high << 8 | ((low + cpu.ReadByteFromRegisterY()) & 0xff);
        var value = cpu.ReadByteFromMemory(address);
        var address2 = ((high << 8 | low) + cpu.ReadByteFromRegisterY()) & 0xffff;

        cpu.ReadyForNextInstruction();
        if (address != address2)
        {
            value = cpu.ReadByteFromMemory(address2);
        }

        cpu.SetValueToAccumulator(value);
        cpu.SetValueToRegisterX(value);

        cpu.SetZeroFlagBasedOn(value);
        cpu.SetNegativeFlagBasedOn(value);
    }
}