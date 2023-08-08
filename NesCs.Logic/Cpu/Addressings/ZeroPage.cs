namespace NesCs.Logic.Cpu.Addressings;

public class ZeroPage : IAddressing
{
    (int, byte) IAddressing.ObtainValueAndAddress(Cpu6502 cpu)
    {
        cpu.ReadyForNextInstruction();
        var address = cpu.ReadByteFromProgram();

        cpu.ReadyForNextInstruction();
        return (address, cpu.ReadByteFromMemory(address));
    }
}
