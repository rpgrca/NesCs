namespace NesCs.Logic.Cpu.Addressings;

public class ZeroPage : IAddressing
{
    byte IAddressing.ObtainValue(Cpu6502 cpu)
    {
        cpu.ReadyForNextInstruction();
        var address = cpu.ReadByteFromProgram();

        cpu.ReadyForNextInstruction();
        return cpu.ReadByteFromMemory(address);
    }
}
