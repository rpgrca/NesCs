namespace NesCs.Logic.Cpu.Operations;

public class ShiftLeft : IOperation
{
    public void Execute(Cpu6502 cpu, byte value, int address)
    {
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