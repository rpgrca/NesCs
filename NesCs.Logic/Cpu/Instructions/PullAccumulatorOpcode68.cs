namespace NesCs.Logic.Cpu.Instructions;

public class PullAccumulatorOpcode68 : IInstruction
{
    public void Execute(Cpu6502 cpu)
    {
        cpu.ReadyForNextInstruction();
        _ = cpu.ReadByteFromMemory(cpu.ReadByteFromProgramCounter());

        _ = cpu.ReadByteFromStackMemory();
        var sp = cpu.ReadByteFromStackPointer();
        sp += 1;
        cpu.SetValueIntoStackPointer(sp);

        var a = cpu.ReadByteFromStackMemory();
        cpu.SetValueIntoAccumulator(a);

        cpu.SetZeroFlagBasedOn(a);
        cpu.SetNegativeFlagBasedOn(a);
    }
}