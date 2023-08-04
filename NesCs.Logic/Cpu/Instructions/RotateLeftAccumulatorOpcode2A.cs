namespace NesCs.Logic.Cpu.Instructions;

public class RotateLeftAccumulatorOpcode2A : IInstruction
{
    public void Execute(Cpu6502 cpu)
    {
        cpu.ReadyForNextInstruction();
        _ = cpu.ReadByteFromMemory(cpu.ReadByteFromProgramCounter());

        var value = cpu.ReadByteFromAccumulator();
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
        cpu.SetValueToAccumulator(result);

        cpu.SetZeroFlagBasedOn(result);
        cpu.SetNegativeFlagBasedOn(result);
    }
}