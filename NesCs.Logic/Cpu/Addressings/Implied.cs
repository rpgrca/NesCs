namespace NesCs.Logic.Cpu.Addressings;

public class Implied : IAddressing
{
    public byte[] PeekOperands(Cpu6502 cpu) => Array.Empty<byte>();

    (int, byte) IAddressing.ObtainValueAndAddress(Cpu6502 cpu)
    {
        cpu.ReadyForNextInstruction();
        var address = cpu.ReadByteFromProgramCounter();
        var value = cpu.ReadByteFromMemory(address);

        return (address, value);
    }
}
