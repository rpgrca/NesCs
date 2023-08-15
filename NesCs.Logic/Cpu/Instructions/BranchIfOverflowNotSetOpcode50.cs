namespace NesCs.Logic.Cpu.Instructions;

public class BranchIfOverflowNotSetOpcode50 : IInstruction
{
    public string Name => "BVC";

    public byte Opcode => 0x50;

    public byte[] PeekOperands(Cpu6502 cpu)
    {
        byte[] operands = { cpu.PeekMemory(cpu.ReadByteFromProgramCounter() + 1) };
        return operands;
    }

    public void Execute(Cpu6502 cpu)
    {
        cpu.ReadyForNextInstruction();
        var offset = (sbyte)cpu.ReadByteFromProgram();

        cpu.ReadyForNextInstruction();
        _ = cpu.ReadByteFromProgramCounter();

        if (! cpu.IsOverflowFlagSet())
        {
            var pc = cpu.ReadByteFromProgramCounter();
            _ = cpu.ReadByteFromMemory(pc);

            var newPc = (pc + offset) & 0xffff;
            cpu.SetValueToProgramCounter(newPc);

            var v = (pc / 256) - (newPc / 256);
            if (v != 0)
            {
                _ = cpu.ReadByteFromMemory(newPc + (v * 256));
            }
        }
    }
}