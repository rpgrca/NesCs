namespace NesCs.Logic.Cpu.Instructions;

public class IncrementMemoryAbsoluteXOpcodeFE : IInstruction
{
    public void Execute(Cpu6502 cpu)
    {
        cpu.ReadyForNextInstruction();
        var low = cpu.ReadByteFromProgram();

        cpu.ReadyForNextInstruction();
        var high = cpu.ReadByteFromProgram();

        cpu.ReadyForNextInstruction();
        var address = high << 8 | low + cpu.ReadByteFromRegisterX() & 0xff;
        var value = cpu.ReadByteFromMemory(address);

        var address2 = (high << 8 | low) + cpu.ReadByteFromRegisterX() & 0xffff;
        if (address != address2)
        {
            value = cpu.ReadByteFromMemory(address2);
            address = address2;
        }
        else
        {
            cpu.ReadByteFromMemory(address);
        }

        cpu.WriteByteToMemory(address, value);
        value++;
        cpu.WriteByteToMemory(address, value);

        cpu.SetZeroFlagBasedOn(value);
        cpu.SetNegativeFlagBasedOn(value);
    }
}