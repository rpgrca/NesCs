using System.Diagnostics;

namespace NesCs.Logic.Cpu.Instructions;

[DebuggerDisplay("{((IDebuggerDisplay)this).Display}")]
internal class ReturnFromSubroutineOpcode60 : IInstruction
{
    public string Name => "RTS";

    public byte Opcode => 0x60;

    string IDebuggerDisplay.Display => $"{Opcode:X2} {Name}";

    public byte[] PeekOperands(Cpu6502 cpu) => Array.Empty<byte>();

    // RTS
    //     #  address R/W description
    //    --- ------- --- -----------------------------------------------
    //     1    PC     R  fetch opcode, increment PC

    public void Execute(Cpu6502 cpu)
    {
        //     #  address R/W description
        //     2    PC     R  read next instruction byte (and throw it away)
        cpu.ReadyForNextInstruction();
        _ = cpu.ReadByteFromMemory(cpu.ReadByteFromProgramCounter());

        //     #  address R/W description
        //     3  $0100,S  R  increment S
        cpu.ReadyForNextInstruction();
        _ = cpu.ReadByteFromStackMemory();
        var sp = cpu.ReadByteFromStackPointer();
        sp += 1;
        cpu.SetValueToStackPointer(sp);

        //     #  address R/W description
        //     4  $0100,S  R  pull PCL from stack, increment S
        cpu.ReadyForNextInstruction();
        var pcl = cpu.ReadByteFromStackMemory();
        sp += 1;
        cpu.SetValueToStackPointer(sp);

        //     #  address R/W description
        //     5  $0100,S  R  pull PCH from stack
        cpu.ReadyForNextInstruction();
        var pch = cpu.ReadByteFromStackMemory();

        //     #  address R/W description
        //     6    PC     R  increment PC
        cpu.ReadyForNextInstruction();
        var address = (pch << 8 | pcl);
        _ = cpu.ReadByteFromMemory(address);
        cpu.SetValueToProgramCounter(address);

        cpu.ReadyForNextInstruction();
    }
}