namespace NesCs.Logic.Cpu.Addressings;

public class ZeroPageYIndexed : IAddressing
{
    (int, byte) IAddressing.ObtainValueAndAddress(Cpu6502 cpu)
    {
        cpu.ReadyForNextInstruction();
        var address = cpu.ReadByteFromProgram();
        _ = cpu.ReadByteFromMemory(address);

        cpu.ReadyForNextInstruction();
        address = (byte)(address + cpu.ReadByteFromRegisterY());
        return (address, cpu.ReadByteFromMemory(address));
    }
}
