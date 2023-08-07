namespace NesCs.Logic.Cpu.Addressings;

public class ZeroPageXIndexed : IAddressing
{
    byte IAddressing.ObtainValue(Cpu6502 cpu)
    {
        cpu.ReadyForNextInstruction();
        var address = cpu.ReadByteFromProgram();
        _ = cpu.ReadByteFromMemory(address);

        cpu.ReadyForNextInstruction();
        return cpu.ReadByteFromMemory((byte)(address + cpu.ReadByteFromRegisterX()));
    }
}
