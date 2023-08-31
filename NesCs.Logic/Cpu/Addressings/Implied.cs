using System.Diagnostics;

namespace NesCs.Logic.Cpu.Addressings;

[DebuggerDisplay("{((IDebuggerDisplay)this).Display}")]
internal class Implied : IAddressing
{
    private readonly Func<Cpu6502, int, byte, byte> _reader;

    string IDebuggerDisplay.Display => string.Empty;

    public byte[] PeekOperands(Cpu6502 cpu) => Array.Empty<byte>();

    public Implied(Func<Cpu6502, int, byte, byte> reader) => _reader = reader;

    (int, byte) IAddressing.ObtainValueAndAddress(Cpu6502 cpu)
    {
        cpu.ReadyForNextInstruction();
        var address = cpu.ReadByteFromProgramCounter();
        var value = cpu.ReadByteFromMemory(address);

        return (address, _reader(cpu, address, value));
    }
}
