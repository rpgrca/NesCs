using System.Diagnostics;

namespace NesCs.Logic.Cpu.Addressings;

[DebuggerDisplay("{((IDebuggerDisplay)this).Display}")]
public class Immediate : IAddressing
{
    string IDebuggerDisplay.Display => "imm";

    public byte[] PeekOperands(Cpu6502 cpu)
    {
        byte[] operands = { cpu.PeekMemory(cpu.ReadByteFromProgramCounter() + 1) };
        return operands;
    }

    (int, byte) IAddressing.ObtainValueAndAddress(Cpu6502 cpu)
    {
        cpu.ReadyForNextInstruction();
        var address = cpu.ReadByteFromProgramCounter();
        var value = cpu.ReadByteFromMemory(address);

        cpu.ReadyForNextInstruction();
        return (address, value);
    }
}