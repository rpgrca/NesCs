namespace NesCs.Logic.Cpu.Addressings;

public class Implied : IAddressing
{
    (int, byte) IAddressing.ObtainValueAndAddress(Cpu6502 cpu)
    {
        cpu.ReadyForNextInstruction();
        _ = cpu.ReadByteFromMemory(cpu.ReadByteFromProgramCounter());

        return (0, 0);
    }
}
