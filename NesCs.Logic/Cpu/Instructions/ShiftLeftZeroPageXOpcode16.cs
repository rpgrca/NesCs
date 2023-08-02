namespace NesCs.Logic.Cpu.Instructions;

public class ShiftLeftZeroPageXOpcode16 : IInstruction
{
    public void Execute(Cpu6502 cpu)
    {
        cpu.ReadyForNextInstruction();
        var address = cpu.ReadByteFromProgram();
        _ = cpu.ReadByteFromMemory(address);

        cpu.ReadyForNextInstruction();
        address = (byte)((address + cpu.ReadByteFromRegisterX()) & 0xff);
        var value = cpu.ReadByteFromMemory(address);

        cpu.WriteByteToMemory(address, value);

        int result = value << 1;
        if ((result >> 8) != 0)
        {
            cpu.SetCarryFlag();
        }
        else
        {
            cpu.ClearCarryFlag();
        }
 
        value = (byte)(result & 0xff);
        cpu.WriteByteToMemory(address, value);
        cpu.SetNegativeFlagBasedOn(value);
        cpu.SetZeroFlagBasedOn(value);
    }
}