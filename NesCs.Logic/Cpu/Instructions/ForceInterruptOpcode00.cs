namespace NesCs.Logic.Cpu.Instructions;

public class ForceInterruptOpcode00 : IInstruction
{
    public string Name => "BRK";

    public byte Opcode => 0x00;

    public void Execute(Cpu6502 cpu)
    {
        cpu.ReadyForNextInstruction();
        var pc = cpu.ReadByteFromProgramCounter();
        _ = cpu.ReadByteFromMemory(pc);

        cpu.ReadyForNextInstruction();
        var pch = (byte)((pc & 0xff00) >> 8);
        var pcl = (byte)((pc & 0xff) + 1);

        if (pcl == 0)
        {
            pch++;
        }

        cpu.WriteByteToStackMemory(pch);
        cpu.WriteByteToStackMemory(pcl);

        var flags = cpu.GetFlags();
        cpu.WriteByteToStackMemory((byte)(flags | ProcessorStatus.B));

        flags |= ProcessorStatus.I;
        cpu.OverwriteFlags(flags);

        pcl = cpu.ReadByteFromMemory(0xfffe);
        pch = cpu.ReadByteFromMemory(0xffff);
        cpu.SetValueToProgramCounter(pch << 8 | pcl);
    }
}