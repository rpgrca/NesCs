namespace NesCs.Logic.Cpu.Addressings;

public class Absolute : IAddressing
{
    private readonly Func<Cpu6502, int, byte> _reader;

    public Absolute(Func<Cpu6502, int, byte> reader) => _reader = reader;

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
        var address = high << 8 | low;
        return (address, _reader(cpu, address));
    }
}