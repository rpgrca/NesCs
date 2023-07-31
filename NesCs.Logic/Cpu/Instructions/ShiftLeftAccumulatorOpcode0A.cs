namespace NesCs.Logic.Cpu.Instructions;

public class ShiftLeftAccumulatorOpcode0A : IInstruction
{
    public void Execute(Cpu6502 cpu)
    {
        cpu.ReadyForNextInstruction();
        var a = cpu.ReadByteFromAccumulator() << 1;

        _ = cpu.ReadByteFromMemory(cpu.ReadByteFromProgramCounter());
        if ((a >> 8) != 0)
        {
            cpu.SetCarryFlag();
        }
        else
        {
            cpu.ClearCarryFlag();
        }
 
        var result = (byte)(a & 0xff);

        cpu.SetValueIntoAccumulator(result);
        cpu.SetNegativeFlagBasedOn(result);
        cpu.SetZeroFlagBasedOn(result);
    }
}