namespace NesCs.Logic.Cpu.Instructions;

public class ShiftRightAccumulatorOpcode4A : IInstruction
{
    public void Execute(Cpu6502 cpu)
    {
        cpu.ReadyForNextInstruction();
        _ = cpu.ReadByteFromMemory(cpu.ReadByteFromProgramCounter());

        var a = cpu.ReadByteFromAccumulator();

        if ((a & 1) == 1)
        {
            cpu.SetCarryFlag();
        }
        else
        {
            cpu.ClearCarryFlag();
        }

        a >>= 1;
        cpu.SetValueToAccumulator(a);
        cpu.SetNegativeFlagBasedOn(a);
        cpu.SetZeroFlagBasedOn(a);
    }
}