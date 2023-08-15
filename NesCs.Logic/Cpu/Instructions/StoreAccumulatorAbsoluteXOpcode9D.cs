namespace NesCs.Logic.Cpu.Instructions;

public class StoreAccumulatorAbsoluteXOpcode9D : IInstruction
{
    public string Name => "STA";

    public byte Opcode => 0x9D;

    public void Execute(Cpu6502 cpu)
    {
        cpu.ReadyForNextInstruction();
        var low = cpu.ReadByteFromProgram();

        cpu.ReadyForNextInstruction();
        var high = cpu.ReadByteFromProgram();

        cpu.ReadyForNextInstruction();
        var address = high << 8 | low + cpu.ReadByteFromRegisterX() & 0xff;
        _ = cpu.ReadByteFromMemory(address);

        var address2 = (high << 8 | low) + cpu.ReadByteFromRegisterX() & 0xffff;
        if (address != address2)
        {
            address = address2;
        }

        cpu.WriteByteToMemory(address, cpu.ReadByteFromAccumulator());
    }
}