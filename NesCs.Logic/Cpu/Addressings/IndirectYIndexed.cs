namespace NesCs.Logic.Cpu.Addressings;

public class IndirectYIndexed : IAddressing
{
    (int, byte) IAddressing.ObtainValueAndAddress(Cpu6502 cpu)
    {
        cpu.ReadyForNextInstruction();
        var address = cpu.ReadByteFromProgram();
        var low = cpu.ReadByteFromMemory(address);

        address = (byte)(address + 1 & 0xff);
        cpu.ReadyForNextInstruction();
        var high = cpu.ReadByteFromMemory(address);

        var effectiveAddress = high << 8 | low + cpu.ReadByteFromRegisterY() & 0xff;
        var value = cpu.ReadByteFromMemory(effectiveAddress);

        var effectiveAddress2 = (high << 8 | low) + cpu.ReadByteFromRegisterY() & 0xffff;
        if (effectiveAddress != effectiveAddress2)
        {
            effectiveAddress = effectiveAddress2;
            value = cpu.ReadByteFromMemory(effectiveAddress);
        }

        return (effectiveAddress, value);
    }
}