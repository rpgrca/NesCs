namespace NesCs.Logic.Cpu.Instructions;

public class IllegalShiftRightXorOpcode47 : IInstruction
{
    public string Name => "SRE";

    public byte Opcode => 0x47;

    public void Execute(Cpu6502 cpu)
    {
        cpu.ReadyForNextInstruction();
        var address = cpu.ReadByteFromProgram();
        var value = cpu.ReadByteFromMemory(address);

        cpu.ReadyForNextInstruction();
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