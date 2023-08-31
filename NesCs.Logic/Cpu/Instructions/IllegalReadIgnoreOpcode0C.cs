using System.Diagnostics;

namespace NesCs.Logic.Cpu.Instructions;

[DebuggerDisplay("{((IDebuggerDisplay)this).Display}")]
internal class IllegalReadIgnoreOpcode0C : IInstruction
{
    public string Name => "IGN";

    public byte Opcode => 0x0C;

    string IDebuggerDisplay.Display => $"{Opcode:X2} {Name} (abs)";

    public byte[] PeekOperands(Cpu6502 cpu) => Array.Empty<byte>();

    public void Execute(Cpu6502 cpu)
    {
        cpu.ReadyForNextInstruction();
        _ = cpu.ReadByteFromMemory(cpu.ReadByteFromProgramCounter());

        cpu.ReadyForNextInstruction();
        _ = cpu.ReadByteFromMemory(cpu.ReadByteFromProgramCounter());

#if NESDEV
        // TODO: Breaks Tom tests but fits golden log
        _ = cpu.ReadByteFromMemory(cpu.ReadByteFromProgramCounter());
#endif

        cpu.ReadyForNextInstruction();
    }
}