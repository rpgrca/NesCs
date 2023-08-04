namespace NesCs.Logic.Cpu.Instructions;

public class JumpToSubroutineOpcode20 : IInstruction
{
    public void Execute(Cpu6502 cpu)
    {
        cpu.ReadyForNextInstruction();
        var low = cpu.ReadByteFromProgram();

        var pc = cpu.ReadByteFromProgramCounter();
        var pch = (byte)((pc & 0xff00) >> 8);
        var pcl = (byte)((pc & 0xff) + 1);

        if (pcl == 0)
        {
            pch++;
        }

        _ = cpu.ReadByteFromStackMemory();

        cpu.WriteByteToStackMemory(pch);
        cpu.WriteByteToStackMemory(pcl);

        cpu.ReadyForNextInstruction();

        var high = cpu.ReadByteFromProgram();
        var address = high << 8 | low;
        cpu.SetValueToProgramCounter(address);
    }
}