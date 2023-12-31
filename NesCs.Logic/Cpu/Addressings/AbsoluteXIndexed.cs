using System.Diagnostics;

namespace NesCs.Logic.Cpu.Addressings;

[DebuggerDisplay("{((IDebuggerDisplay)this).Display}")]
internal class AbsoluteXIndexed : IAddressing
{
    private readonly Action<Cpu6502, int> _readWhenInSamePage;
    private readonly Func<Cpu6502, int, byte, byte> _readWhenInDifferentPage;

    string IDebuggerDisplay.Display => "(abx)";

    public byte[] PeekOperands(Cpu6502 cpu)
    {
        byte[] operands = { cpu.PeekMemory(cpu.ReadByteFromProgramCounter() + 1), cpu.PeekMemory(cpu.ReadByteFromProgramCounter() + 2) };
        return operands;
    }

    public AbsoluteXIndexed(Func<Cpu6502, int, byte, byte> readWhenInDifferentPage, Action<Cpu6502, int> readWhenInSamePage)
    {
        _readWhenInDifferentPage = readWhenInDifferentPage;
        _readWhenInSamePage = readWhenInSamePage;
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
            value = _readWhenInDifferentPage(cpu, address, value);
        }
        else
        {
            _readWhenInSamePage(cpu, address);
        }

        return (address, value);
    }
}