namespace NesCs.Logic.Cpu.Addressings;

public class Immediate : IAddressing
{
    byte IAddressing.ObtainValue(Cpu6502 cpu)
    {
        cpu.ReadyForNextInstruction();
        var value = cpu.ReadByteFromProgram();

        cpu.ReadyForNextInstruction();
        return value;
    }
}
