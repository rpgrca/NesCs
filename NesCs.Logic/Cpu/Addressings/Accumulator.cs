using System.Diagnostics;

namespace NesCs.Logic.Cpu.Addressings;

[DebuggerDisplay("acc")]
public class Accumulator : IAddressing
{
    string IDebuggerDisplay.Display => string.Empty;

    public byte[] PeekOperands(Cpu6502 cpu) => Array.Empty<byte>();

    (int, byte) IAddressing.ObtainValueAndAddress(Cpu6502 cpu)
    {
        cpu.ReadyForNextInstruction();
        _ = cpu.ReadByteFromMemory(cpu.ReadByteFromProgramCounter());

        return (0, cpu.ReadByteFromAccumulator());
    }
}
