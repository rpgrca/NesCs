namespace NesCs.Logic.Cpu.Instructions;

public class RotateLeftZeroPageXOpcode36 : IInstruction
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
        int rotatedValue = (value << 1) | (cpu.IsReadCarryFlagSet()? 1 : 0);

        if ((rotatedValue >> 8) != 0)
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