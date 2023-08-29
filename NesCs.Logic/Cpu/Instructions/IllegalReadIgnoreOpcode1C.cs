using System.Diagnostics;

namespace NesCs.Logic.Cpu.Instructions;

[DebuggerDisplay("{((IDebuggerDisplay).Display)}")]
public class IllegalReadIgnoreOpcode1C : IInstruction
{
    public virtual string Name => "IGN";

    public virtual byte Opcode => 0x1C;

    string IDebuggerDisplay.Display => $"{Opcode:X2} {Name} (abx)";

    public byte[] PeekOperands(Cpu6502 cpu)
    {
        byte[] operands = { cpu.PeekMemory(cpu.ReadByteFromProgramCounter() + 1), cpu.PeekMemory(cpu.ReadByteFromProgramCounter() + 2) };
        return operands;
    }

    public void Execute(Cpu6502 cpu)
    {
        cpu.ReadyForNextInstruction();
        var low = cpu.ReadByteFromProgram();

        cpu.ReadyForNextInstruction();
        var high = cpu.ReadByteFromProgram();

        int address = 0;

#if NESDEV
        // TODO: Breaks Tom's tests
        address = high << 8 | low + cpu.ReadByteFromRegisterX();
        _ = cpu.ReadByteFromMemory(address);

        if (low + cpu.ReadByteFromRegisterX() > 255)
        {
#endif
            address = high << 8 | ((low + cpu.ReadByteFromRegisterX()) & 255);
            _ = cpu.ReadByteFromMemory(address);
#if NESDEV
        }
#endif

        cpu.ReadyForNextInstruction();
    }
}