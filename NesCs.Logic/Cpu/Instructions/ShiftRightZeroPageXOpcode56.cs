namespace NesCs.Logic.Cpu.Instructions;

public class ShiftRightZeroPageXOpcode56 : IInstruction
{
    public void Execute(Cpu6502 cpu)
    {
        cpu.ReadyForNextInstruction();
        var address = cpu.ReadByteFromProgram();
        _ = cpu.ReadByteFromMemory(address);

        cpu.ReadyForNextInstruction();
        address = (byte)(address + cpu.ReadByteFromRegisterX());
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
        cpu.SetNegativeFlagBasedOn(value);
        cpu.SetZeroFlagBasedOn(value);
    }
}