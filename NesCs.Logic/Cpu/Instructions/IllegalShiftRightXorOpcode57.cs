namespace NesCs.Logic.Cpu.Instructions;

public class IllegalShiftRightXorOpcode57 : IInstruction
{
    public string Name => "SRE";

    public byte Opcode => 0x57;

    public void Execute(Cpu6502 cpu)
    {
        cpu.ReadyForNextInstruction();
        var address = cpu.ReadByteFromProgram();
        _ = cpu.ReadByteFromMemory(address);

        address = (byte)(address + cpu.ReadByteFromRegisterX());
        cpu.ReadyForNextInstruction();
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