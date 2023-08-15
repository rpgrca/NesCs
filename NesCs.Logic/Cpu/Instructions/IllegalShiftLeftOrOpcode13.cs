namespace NesCs.Logic.Cpu.Instructions;

public class IllegalShiftLeftOrOpcode13 : IInstruction
{
    public string Name => "SLO";

    public byte Opcode => 0x13;

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

        var operand = value << 1;
        if ((operand >> 8) != 0)
        {
            cpu.SetCarryFlag();
        }
        else
        {
            cpu.ClearCarryFlag();
        }

        var result = (byte)(operand & 0xff);
        cpu.WriteByteToMemory(effectiveAddress, result);

        result = (byte)(cpu.ReadByteFromAccumulator() | operand);
        cpu.SetValueToAccumulator(result);
        cpu.SetNegativeFlagBasedOn(result);
        cpu.SetZeroFlagBasedOn(result);
    }
}