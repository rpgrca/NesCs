using System.Diagnostics;

namespace NesCs.Logic.Cpu.Addressings;

[DebuggerDisplay("{((IDebuggerDisplay)this).Display}")]
public class Relative : IAddressing
{
    string IDebuggerDisplay.Display => "(rel)";

    public byte[] PeekOperands(Cpu6502 cpu)
    {
        byte[] operands = { cpu.PeekMemory(cpu.ReadByteFromProgramCounter() + 1) };
        return operands;
    }

    public (int Address, byte Value) ObtainValueAndAddress(Cpu6502 cpu)
    {
        cpu.ReadyForNextInstruction();
        var offset = (sbyte)cpu.ReadByteFromProgram();

        cpu.ReadyForNextInstruction();
        _ = cpu.ReadByteFromProgramCounter();

        return (offset, 0);
    }
}