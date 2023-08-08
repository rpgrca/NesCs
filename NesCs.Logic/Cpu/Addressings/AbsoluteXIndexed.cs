namespace NesCs.Logic.Cpu.Addressings;

public class AbsoluteXIndexed : IAddressing
{
    private readonly Action<Cpu6502, int> _extraRead;

    public AbsoluteXIndexed(Action<Cpu6502, int> extraRead) => _extraRead = extraRead;

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
            value = cpu.ReadByteFromMemory(address);
        }
        else
        {
            _extraRead(cpu, address);
        }

        return (address, value);
    }
}