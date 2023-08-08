namespace NesCs.Logic.Cpu.Addressings;

public class Accumulator : IAddressing
{
    (int, byte) IAddressing.ObtainValueAndAddress(Cpu6502 cpu)
    {
        cpu.ReadyForNextInstruction();
        _ = cpu.ReadByteFromMemory(cpu.ReadByteFromProgramCounter());

        return (0, cpu.ReadByteFromAccumulator());
    }
}
