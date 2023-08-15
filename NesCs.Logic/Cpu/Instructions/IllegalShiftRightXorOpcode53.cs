namespace NesCs.Logic.Cpu.Instructions;

public class IllegalShiftRightXorOpcode53 : IInstruction
{
    public string Name => "SRE";

    public byte Opcode => 0x53;

    public byte[] PeekOperands(Cpu6502 cpu)
    {
        byte[] operands = { cpu.PeekMemory(cpu.ReadByteFromProgramCounter() + 1) };
        return operands;
    }

    public void Execute(Cpu6502 cpu)
    {
        cpu.ReadyForNextInstruction();
        var address = cpu.ReadByteFromProgram();
        var low = cpu.ReadByteFromMemory(address);

        address = (byte)(address + 1);
        cpu.ReadyForNextInstruction();
        var high = cpu.ReadByteFromMemory(address);

        var effectiveAddress = high << 8 | ((low + cpu.ReadByteFromRegisterY()) & 0xff);
        _ = cpu.ReadByteFromMemory(effectiveAddress);

        effectiveAddress = ((high << 8 | low) + cpu.ReadByteFromRegisterY()) & 0xffff;
        var value = cpu.ReadByteFromMemory(effectiveAddress);
        cpu.WriteByteToMemory(effectiveAddress, value);

        if ((value & 1) == 1)
        {
            cpu.SetCarryFlag();
        }
        else
        {
            cpu.ClearCarryFlag();
        }

        value >>= 1;
        cpu.WriteByteToMemory(effectiveAddress, value);

        value ^= cpu.ReadByteFromAccumulator();
        cpu.SetValueToAccumulator(value);
        cpu.SetNegativeFlagBasedOn(value);
        cpu.SetZeroFlagBasedOn(value);
    }
}