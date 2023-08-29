using System.Diagnostics;

namespace NesCs.Logic.Cpu.Addressings;

[DebuggerDisplay("{((IDebuggerDisplay)this).Display}")]
public class ZeroPageXIndexed : IAddressing
{
    private readonly Func<Cpu6502, int, byte> _reader;

    string IDebuggerDisplay.Display => "zpx";

    public byte[] PeekOperands(Cpu6502 cpu)
    {
        byte[] operands = { cpu.PeekMemory(cpu.ReadByteFromProgramCounter() + 1) };
        return operands;
    }

    public ZeroPageXIndexed(Func<Cpu6502, int, byte> reader) =>
        _reader = reader;

    (int, byte) IAddressing.ObtainValueAndAddress(Cpu6502 cpu)
    {
        cpu.ReadyForNextInstruction();
        var address = cpu.ReadByteFromProgram();
        _ = cpu.ReadByteFromMemory(address);

        cpu.ReadyForNextInstruction();
        address = (byte)(address + cpu.ReadByteFromRegisterX());
        return (address, _reader(cpu, address));
    }
}