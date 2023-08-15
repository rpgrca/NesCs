namespace NesCs.Logic.Cpu.Addressings;

public class Immediate : IAddressing
{
    public byte[] PeekOperands(Cpu6502 cpu)
    {
        byte[] operands = { cpu.PeekMemory(cpu.ReadByteFromProgramCounter() + 1) };
        return operands;
    }

    (int, byte) IAddressing.ObtainValueAndAddress(Cpu6502 cpu)
    {
        cpu.ReadyForNextInstruction();
        var address = cpu.ReadByteFromProgramCounter();
        var value = cpu.ReadByteFromMemory(address);

        cpu.ReadyForNextInstruction();
        return (address, value);
    }
}