namespace NesCs.Logic.Cpu.Addressings;

public class Indirect : IAddressing
{
    public byte[] PeekOperands(Cpu6502 cpu)
    {
        byte[] operands = { cpu.PeekMemory(cpu.ReadByteFromProgramCounter() + 1), cpu.PeekMemory(cpu.ReadByteFromProgramCounter() + 2) };
        return operands;
    }

    (int, byte) IAddressing.ObtainValueAndAddress(Cpu6502 cpu)
    {
        cpu.ReadyForNextInstruction();
        var low = cpu.ReadByteFromProgram();

        cpu.ReadyForNextInstruction();
        var high = cpu.ReadByteFromProgram();

        cpu.ReadyForNextInstruction();
        var address = high << 8 | low;
 
        var newLow = cpu.ReadByteFromMemory(address);
        var newHigh = low == 0xff ? cpu.ReadByteFromMemory(high << 8) : (int)cpu.ReadByteFromMemory(address + 1);

        address = newHigh << 8 | newLow;
        return (address, 0);
    }
}
