namespace NesCs.Logic.Cpu.Instructions;

public class BranchIfCarryNotSetOpcode90 : IInstruction
{
    public string Name => "BCC";

    public byte Opcode => 0x90;

    public void Execute(Cpu6502 cpu)
    {
        cpu.ReadyForNextInstruction();
        var offset = (sbyte)cpu.ReadByteFromProgram();

        cpu.ReadyForNextInstruction();
        _ = cpu.ReadByteFromProgramCounter();

        if (! cpu.IsReadCarryFlagSet())
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