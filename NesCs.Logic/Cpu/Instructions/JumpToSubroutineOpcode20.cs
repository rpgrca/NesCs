using System.Diagnostics;

namespace NesCs.Logic.Cpu.Instructions;

[DebuggerDisplay("{((IDebuggerDisplay)this).Display}")]
internal class JumpToSubroutineOpcode20 : IInstruction
{
    public string Name => "JSR";

    public byte Opcode => 0x20;

    string IDebuggerDisplay.Display => $"{Opcode:X2} {Name} abs";

    public byte[] PeekOperands(Cpu6502 cpu)
    {
        byte[] operands = { cpu.PeekMemory(cpu.ReadByteFromProgramCounter() + 1), cpu.PeekMemory(cpu.ReadByteFromProgramCounter() + 2) };
        return operands;
    }

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