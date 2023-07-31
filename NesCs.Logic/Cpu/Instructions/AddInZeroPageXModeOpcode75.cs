namespace NesCs.Logic.Cpu.Instructions;

public class AddInZeroPageXModeOpcode75 : IInstruction
{
    public void Execute(Cpu6502 cpu)
    {
        cpu.ReadyForNextInstruction();
        var address = cpu.ReadByteFromProgram();
        _ = cpu.ReadByteFromMemory(address);

        cpu.ReadyForNextInstruction();
        var value = cpu.ReadByteFromMemory((byte)(address + cpu.ReadByteFromRegisterX()));

        var a = cpu.ReadByteFromAccumulator();
        var sum = a + value + (cpu.ReadCarryFlag()? 1 : 0);
        var result = (byte)(sum & 0xff);

        cpu.SetValueIntoAccumulator(result);
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