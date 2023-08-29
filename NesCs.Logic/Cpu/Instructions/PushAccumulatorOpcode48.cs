using System.Diagnostics;

namespace NesCs.Logic.Cpu.Instructions;

[DebuggerDisplay("{((IDebuggerDisplay)this).Display}")]
public class PushAccumulatorOpcode48 : IInstruction
{
    public string Name => "PHA";

    public byte Opcode => 0x48;

    string IDebuggerDisplay.Display => $"{Opcode:X2} {Name}";

    public byte[] PeekOperands(Cpu6502 cpu) => Array.Empty<byte>();

    public void Execute(Cpu6502 cpu)
    {
        cpu.ReadyForNextInstruction();
        _ = cpu.ReadByteFromMemory(cpu.ReadByteFromProgramCounter());

        cpu.WriteByteToStackMemory(cpu.ReadByteFromAccumulator());
    }
}