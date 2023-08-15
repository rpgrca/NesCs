namespace NesCs.Logic.Cpu.Addressings;

public class ZeroPage : IAddressing
{
    private readonly Func<Cpu6502, int, byte> _reader;

    public byte[] PeekOperands(Cpu6502 cpu)
    {
        byte[] operands = { cpu.PeekMemory(cpu.ReadByteFromProgramCounter() + 1) };
        return operands;
    }

    public ZeroPage(Func<Cpu6502, int, byte> reader) =>
        _reader = reader;

    (int, byte) IAddressing.ObtainValueAndAddress(Cpu6502 cpu)
    {
        cpu.ReadyForNextInstruction();
        var address = cpu.ReadByteFromProgram();

        cpu.ReadyForNextInstruction();
        return (address, _reader(cpu, address));
    }
}