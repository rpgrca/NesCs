using System.Diagnostics;

namespace NesCs.Logic.Cpu.Addressings;

[DebuggerDisplay("{((IDebuggerDisplay)this).Display}")]
internal class AbsoluteXIndexedAccumulator : IAddressing
{
    string IDebuggerDisplay.Display => "abx (acc)";

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

        cpu.ReadyForNextInstruction();
        var address = high << 8 | low + cpu.ReadByteFromRegisterX() & 0xff;
        var value = cpu.ReadByteFromMemory(address);

        var pageJumpAddress = (high << 8 | low) + cpu.ReadByteFromRegisterX() & 0xffff;
        if (address != pageJumpAddress)
        {
            address = pageJumpAddress;
        }

        return (address, cpu.ReadByteFromAccumulator());
    }
}