namespace NesCs.Logic.Cpu.Instructions;

public class IllegalShiftRightXorOpcode4F : IInstruction
{
    public string Name => "SRE";

    public byte Opcode => 0x4F;

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

        cpu.ReadyForNextInstruction();
        var address = high << 8 | low;
        var value = cpu.ReadByteFromMemory(address);
        cpu.WriteByteToMemory(address, value);

        if ((value & 1) == 1)
        {
            cpu.SetCarryFlag();
        }
        else
        {
            cpu.ClearCarryFlag();
        }

        value >>= 1;
        cpu.WriteByteToMemory(address, value);

        value ^= cpu.ReadByteFromAccumulator();
        cpu.SetValueToAccumulator(value);
        cpu.SetNegativeFlagBasedOn(value);
        cpu.SetZeroFlagBasedOn(value);
    }
}