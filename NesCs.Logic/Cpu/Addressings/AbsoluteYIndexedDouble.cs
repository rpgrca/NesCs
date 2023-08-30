using System.Diagnostics;

namespace NesCs.Logic.Cpu.Addressings;

[DebuggerDisplay("{((IDebuggerDisplay)this).Display}")]
public class AbsoluteYIndexedDouble : IAddressing
{
    string IDebuggerDisplay.Display => "(aby)";

    public byte[] PeekOperands(Cpu6502 cpu)
    {
        byte[] operands = { cpu.PeekMemory(cpu.ReadByteFromProgramCounter() + 1), cpu.PeekMemory(cpu.ReadByteFromProgramCounter() + 2) };
        return operands;
    }

    (int, byte) IAddressing.ObtainValueAndAddress(Cpu6502 cpu)
    {
        cpu.ReadyForNextInstruction();
        var low = cpu.ReadByteFromProgram();

        cpu.ReadyForNextInstruction();
        var high = cpu.ReadByteFromProgram();

        var address = high << 8 | ((low + cpu.ReadByteFromRegisterY()) & 0xff);
        _ = cpu.ReadByteFromMemory(address);

        address = ((high << 8 | low) + cpu.ReadByteFromRegisterY()) & 0xffff;
        cpu.ReadyForNextInstruction();

        var value = cpu.ReadByteFromMemory(address);

        return (address, value);
    }
}