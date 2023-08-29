using System.Diagnostics;

namespace NesCs.Logic.Cpu.Addressings;

 [DebuggerDisplay("{((IDebuggerDisplay).Display)}")]
public class ZeroPageYIndexed : IAddressing
{
    private readonly Func<Cpu6502, int, byte> _reader;

    string IDebuggerDisplay.Display => "zpy";

    public byte[] PeekOperands(Cpu6502 cpu)
    {
        byte[] operands = { cpu.PeekMemory(cpu.ReadByteFromProgramCounter() + 1) };
        return operands;
    }

    public ZeroPageYIndexed(Func<Cpu6502, int, byte> reader) =>
        _reader = reader;

    (int, byte) IAddressing.ObtainValueAndAddress(Cpu6502 cpu)
    {
        cpu.ReadyForNextInstruction();
        var address = cpu.ReadByteFromProgram();
        _ = cpu.ReadByteFromMemory(address);

        cpu.ReadyForNextInstruction();
        address = (byte)(address + cpu.ReadByteFromRegisterY());
        return (address, _reader(cpu, address));
    }
}