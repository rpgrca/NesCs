using NesCs.Logic.Cpu.Instructions;

namespace NesCs.Logic.Cpu;

internal class IllegalReadIgnoreOpcode3C : IInstruction
{
    public string Name => "IGN";

    public byte Opcode => 0x3C;

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

        address = high << 8 | (low + cpu.ReadByteFromRegisterX()) & 0xff;
        _ = cpu.ReadByteFromMemory(address);

        if (low + cpu.ReadByteFromRegisterX() > 255)
        {
            address = (high << 8 | low) + cpu.ReadByteFromRegisterX();
        }

        _ = cpu.ReadByteFromMemory(address);

        cpu.ReadyForNextInstruction();
    }

}