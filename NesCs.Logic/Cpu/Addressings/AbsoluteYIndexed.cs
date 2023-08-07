namespace NesCs.Logic.Cpu.Addressings;

public class AbsoluteYIndexed : IAddressing
{
    (int, byte) IAddressing.ObtainValueAndAddress(Cpu6502 cpu)
    {
        cpu.ReadyForNextInstruction();
        var low = cpu.ReadByteFromProgram();

        cpu.ReadyForNextInstruction();
        var high = cpu.ReadByteFromProgram();

        cpu.ReadyForNextInstruction();
        var address = high << 8 | low + cpu.ReadByteFromRegisterY() & 0xff;
        var value = cpu.ReadByteFromMemory(address);

        var pageJumpAddress = (high << 8 | low) + cpu.ReadByteFromRegisterY() & 0xffff;
        if (address != pageJumpAddress)
        {
            address = pageJumpAddress;
            value = cpu.ReadByteFromMemory(address);
        }

        return (address, value);
    }
}