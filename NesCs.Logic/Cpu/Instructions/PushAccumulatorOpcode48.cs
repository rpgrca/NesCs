namespace NesCs.Logic.Cpu.Instructions;

public class PushAccumulatorOpcode48 : IInstruction
{
    public void Execute(Cpu6502 cpu)
    {
        cpu.ReadyForNextInstruction();
        _ = cpu.ReadByteFromMemory(cpu.ReadByteFromProgramCounter());

        cpu.WriteByteToStackMemory(cpu.ReadByteFromAccumulator());
    }
}