namespace NesCs.Logic.Cpu.Instructions;

public class AddInAbsoluteXModeOpcode7D : IInstruction
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
        }

        var a = cpu.ReadByteFromAccumulator();
        var sum = a + value + (cpu.IsReadCarryFlagSet()? 1 : 0);
        var result = (byte)(sum & 0xff);

        cpu.SetValueToAccumulator(result);
        cpu.ClearCarryFlag();
        cpu.ClearNegativeFlag();
        cpu.ClearOverflowFlag();
        cpu.ClearZeroFlag();

        // TODO: Not a real 8-bit implementation but works for the time being
        if ((sum >> 8) != 0)
        {
            cpu.SetCarryFlag();
        }

        if (((a ^ result) & (value ^ result) & 0x80) != 0)
        {
            cpu.SetOverflowFlag();
        }

        cpu.SetZeroFlagBasedOn(result);
        cpu.SetNegativeFlagBasedOn(result);
    }
}