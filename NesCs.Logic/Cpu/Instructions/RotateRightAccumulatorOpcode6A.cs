namespace NesCs.Logic.Cpu.Instructions;

public class RotateRightAccumulatorOpcode6A : IInstruction
{
    public void Execute(Cpu6502 cpu)
    {
        cpu.ReadyForNextInstruction();
        _ = cpu.ReadByteFromMemory(cpu.ReadByteFromProgramCounter());

        var value = cpu.ReadByteFromAccumulator();
        var newCarry = (value & 1) == 1;

        var rotatedValue = (byte)(value >> 1);
        if (cpu.IsReadCarryFlagSet())
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

        cpu.SetValueToAccumulator(rotatedValue);
        cpu.SetZeroFlagBasedOn(rotatedValue);
        cpu.SetNegativeFlagBasedOn(rotatedValue);
    }
}