namespace NesCs.Logic.Cpu.Instructions;

public class StoreAccumulatorAbsoluteOpcode8D : IInstruction
{
    public void Execute(Cpu6502 cpu)
    {
        cpu.ReadyForNextInstruction();
        var low = cpu.ReadByteFromProgram();

        cpu.ReadyForNextInstruction();
        var high = cpu.ReadByteFromProgram();

        cpu.ReadyForNextInstruction();
        var address = high << 8 | low;
        cpu.WriteByteToMemory(address, cpu.ReadByteFromAccumulator());
    }
}