namespace NesCs.Logic.Cpu.Addressings;

public class ZeroPage : IAddressing
{
    private readonly Func<Cpu6502, int, byte> _reader;

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