namespace NesCs.Logic.Cpu.Instructions;

public class IllegalRotateRightAddOpcode77 : IInstruction
{
    public string Name => "RRA";

    public byte Opcode => 0x77;

    public byte[] PeekOperands(Cpu6502 cpu)
    {
        byte[] operands = { cpu.PeekMemory(cpu.ReadByteFromProgramCounter() + 1) };
        return operands;
    }

    public void Execute(Cpu6502 cpu)
    {
        cpu.ReadyForNextInstruction();
        var address = cpu.ReadByteFromProgram();
        _ = cpu.ReadByteFromMemory(address);

        address = (byte)(address + cpu.ReadByteFromRegisterX());
        cpu.ReadyForNextInstruction();
        var value = cpu.ReadByteFromMemory(address);
        cpu.WriteByteToMemory(address, value);

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
        cpu.WriteByteToMemory(address, value);

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