namespace NesCs.Logic.Cpu.Instructions;

public class IllegalShiftLeftOrOpcode1B : IInstruction
{
    public string Name => "SLO";

    public byte Opcode => 0x1B;

    public byte[] PeekOperands(Cpu6502 cpu)
    {
        byte[] operands = { cpu.PeekMemory(cpu.ReadByteFromProgramCounter() + 1), cpu.PeekMemory(cpu.ReadByteFromProgramCounter() + 2) };
        return operands;
    }

    public void Execute(Cpu6502 cpu)
    {
        cpu.ReadyForNextInstruction();
        var low = cpu.ReadByteFromProgram();

        cpu.ReadyForNextInstruction();
        var high = cpu.ReadByteFromProgram();

        var address = high << 8 | ((low + cpu.ReadByteFromRegisterY()) & 0xff);
        _ = cpu.ReadByteFromMemory(address);

        address = ((high << 8 | low) + cpu.ReadByteFromRegisterY()) & 0xffff;
        cpu.ReadyForNextInstruction();

        var value = cpu.ReadByteFromMemory(address);
        cpu.WriteByteToMemory(address, value);

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
        cpu.WriteByteToMemory(address, result);

        result = (byte)(cpu.ReadByteFromAccumulator() | operand);
        cpu.SetValueToAccumulator(result);
        cpu.SetNegativeFlagBasedOn(result);
        cpu.SetZeroFlagBasedOn(result);
    }
}