namespace NesCs.Logic.Cpu.Instructions;

public class IllegalShiftLeftOrOpcode17 : IInstruction
{
    public string Name => "SLO";

    public byte Opcode => 0x17;

    public void Execute(Cpu6502 cpu)
    {
        cpu.ReadyForNextInstruction();
        var address = cpu.ReadByteFromProgram();
        _ = cpu.ReadByteFromMemory(address);

        address = (byte)(address + cpu.ReadByteFromRegisterX());
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