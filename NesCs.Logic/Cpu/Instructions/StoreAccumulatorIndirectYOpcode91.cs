namespace NesCs.Logic.Cpu.Instructions;

public class StoreAccumulatorIndirectYOpcode91 : IInstruction
{
    public string Name => "STA";

    public byte Opcode => 0x91;

    public byte[] PeekOperands(Cpu6502 cpu)
    {
        byte[] operands = { cpu.PeekMemory(cpu.ReadByteFromProgramCounter() + 1) };
        return operands;
    }

    public void Execute(Cpu6502 cpu)
    {
        cpu.ReadyForNextInstruction();
        var address = cpu.ReadByteFromProgram();
        var low = cpu.ReadByteFromMemory(address);

        address = (byte)(address + 1 & 0xff);
        cpu.ReadyForNextInstruction();
        var high = cpu.ReadByteFromMemory(address);

        var effectiveAddress = high << 8 | low + cpu.ReadByteFromRegisterY() & 0xff;
        _ = cpu.ReadByteFromMemory(effectiveAddress);

        var effectiveAddress2 = (high << 8 | low) + cpu.ReadByteFromRegisterY() & 0xffff;
        if (effectiveAddress != effectiveAddress2)
        {
            effectiveAddress = effectiveAddress2;
        }

        cpu.WriteByteToMemory(effectiveAddress, cpu.ReadByteFromAccumulator());
    }
}