namespace NesCs.Logic.Cpu.Addressings;

public class IndirectXIndexed : IAddressing
{
    private readonly Func<Cpu6502, int, byte> _reader;

    public IndirectXIndexed(Func<Cpu6502, int, byte> reader) => _reader = reader;

    (int, byte) IAddressing.ObtainValueAndAddress(Cpu6502 cpu)
    {
        cpu.ReadyForNextInstruction();
        var address = cpu.ReadByteFromProgram();
        _ = cpu.ReadByteFromMemory(address);

        address = (byte)(address + cpu.ReadByteFromRegisterX());
        cpu.ReadyForNextInstruction();
        var low = cpu.ReadByteFromMemory(address);

        address = (byte)(address + 1);
        var high = cpu.ReadByteFromMemory(address);

        var effectiveAddress = high << 8 | low;
        return (effectiveAddress, _reader(cpu, effectiveAddress));
    }
}