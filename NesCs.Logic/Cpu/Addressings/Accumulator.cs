namespace NesCs.Logic.Cpu.Addressings;

public class Accumulator : IAddressing
{
    public byte[] PeekOperands(Cpu6502 cpu) => Array.Empty<byte>();

    (int, byte) IAddressing.ObtainValueAndAddress(Cpu6502 cpu)
    {
        cpu.ReadyForNextInstruction();
        _ = cpu.ReadByteFromMemory(cpu.ReadByteFromProgramCounter());

        return (0, cpu.ReadByteFromAccumulator());
    }
}
