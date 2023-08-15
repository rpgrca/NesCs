namespace NesCs.Logic.Cpu.Instructions;

public class IllegalRotateRightAddOpcode63 : IInstruction
{
    public string Name => "RRA";

    public byte Opcode => 0x63;

    public void Execute(Cpu6502 cpu)
    {
        cpu.ReadyForNextInstruction();
        var address = cpu.ReadByteFromProgram();
        _ = cpu.ReadByteFromMemory(address);

        address = (byte)(address + cpu.ReadByteFromRegisterX());
        cpu.ReadyForNextInstruction();
        var low = cpu.ReadByteFromMemory(address);

        address = (byte)(address + 1);
        var high = cpu.ReadByteFromMemory(address);

        var effectiveAddress = high << 8 | low;
        var value = cpu.ReadByteFromMemory(effectiveAddress);
        cpu.WriteByteToMemory(effectiveAddress, value);

        var newCarry = (value & 1) == 1;

        var rotatedValue = (byte)(value >> 1);
        if (cpu.IsReadCarryFlagSet())
        {
            rotatedValue |= 1 << 7;
        }

        if (newCarry)
        {
            cpu.SetCarryFlag();
        }
        else
        {
            cpu.ClearCarryFlag();
        }

        value = (byte)(rotatedValue & 0xff);
        cpu.WriteByteToMemory(effectiveAddress, value);

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