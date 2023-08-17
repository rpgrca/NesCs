namespace NesCs.Logic.Cpu.Addressings;

public class AbsoluteYIndexedAccumulator : IAddressing
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
        var address = high << 8 | low + cpu.ReadByteFromRegisterY() & 0xff;
        _ = cpu.ReadByteFromMemory(address);

        var address2 = (high << 8 | low) + cpu.ReadByteFromRegisterY() & 0xffff;
        if (address != address2)
        {
            address = address2;
        }

        return (address, cpu.ReadByteFromAccumulator());
    }
}