namespace NesCs.Logic.Cpu.Addressings;

public class Absolute : IAddressing
{
    byte IAddressing.ObtainValue(Cpu6502 cpu)
    {
        cpu.ReadyForNextInstruction();
        var low = cpu.ReadByteFromProgram();

        cpu.ReadyForNextInstruction();
        var high = cpu.ReadByteFromProgram();

        cpu.ReadyForNextInstruction();
        return cpu.ReadByteFromMemory(high << 8 | low);
    }
}
