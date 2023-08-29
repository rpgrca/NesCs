using System.Diagnostics;

namespace NesCs.Logic.Cpu.Instructions;

[DebuggerDisplay("{((IDebuggerDisplay)this).Display}")]
public class PushProcessorStatusOpcode08 : IInstruction
{
    public string Name => "PHP";

    public byte Opcode => 0x08;

    string IDebuggerDisplay.Display => $"{Opcode:X2} {Name}";

    public byte[] PeekOperands(Cpu6502 cpu) => Array.Empty<byte>();

    public void Execute(Cpu6502 cpu)
    {
        cpu.ReadyForNextInstruction();
        _ = cpu.ReadByteFromMemory(cpu.ReadByteFromProgramCounter());

        var pc = cpu.GetFlags() | ProcessorStatus.B;
        cpu.WriteByteToStackMemory((byte)pc);
    }
}