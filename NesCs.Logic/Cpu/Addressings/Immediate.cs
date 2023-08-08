namespace NesCs.Logic.Cpu.Addressings;

public class Immediate : IAddressing
{
    (int, byte) IAddressing.ObtainValueAndAddress(Cpu6502 cpu)
    {
        cpu.ReadyForNextInstruction();
        var address = cpu.ReadByteFromProgramCounter();
        var value = cpu.ReadByteFromMemory(address);

        cpu.ReadyForNextInstruction();
        return (address, value);
    }
}