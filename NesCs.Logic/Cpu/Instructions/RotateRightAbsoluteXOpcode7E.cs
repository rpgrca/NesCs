namespace NesCs.Logic.Cpu.Instructions;

public class RotateRightAbsoluteXOpcode7E : IInstruction
{
    public void Execute(Cpu6502 cpu)
    {
        cpu.ReadyForNextInstruction();
        var low = cpu.ReadByteFromProgram();

        cpu.ReadyForNextInstruction();
        var high = cpu.ReadByteFromProgram();

        cpu.ReadyForNextInstruction();
        var address = high << 8 | low + cpu.ReadByteFromRegisterX() & 0xff;
        var value = cpu.ReadByteFromMemory(address);

        var address2 = (high << 8 | low) + cpu.ReadByteFromRegisterX() & 0xffff;
        if (address != address2)
        {
            value = cpu.ReadByteFromMemory(address2);
            address = address2;
        }
        else
        {
            _ = cpu.ReadByteFromMemory(address);
        }

        cpu.WriteByteToMemory(address, value);

        var newCarry = (value & 1) == 1;

        var rotatedValue = (byte)(value >> 1);
        if (cpu.ReadCarryFlag())
        {
            rotatedValue |= 1 << 7;
        }

        if (newCarry)
        {
            cpu.SetCarryFlag();
        }
        else
        {
            cpu.ClearCarryFlag();
        }

        var result = (byte)(rotatedValue & 0xff);
        cpu.WriteByteToMemory(address, result);

        cpu.SetZeroFlagBasedOn(result);
        cpu.SetNegativeFlagBasedOn(result);
    }
}