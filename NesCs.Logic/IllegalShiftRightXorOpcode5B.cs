namespace NesCs.Logic.Cpu.Instructions;

public class IllegalShiftRightXorOpcode5B : IInstruction
{
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