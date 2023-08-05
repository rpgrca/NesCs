namespace NesCs.Logic.Cpu.Instructions;

public class ShiftLeftZeroPageOpcode06 : IInstruction
{
    public void Execute(Cpu6502 cpu)
    {
        cpu.ReadyForNextInstruction();
        var address = cpu.ReadByteFromProgram();

        cpu.ReadyForNextInstruction();
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