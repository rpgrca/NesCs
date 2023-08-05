namespace NesCs.Logic.Cpu.Instructions;

public class PushProcessorStatusOpcode08 : IInstruction
{
    public void Execute(Cpu6502 cpu)
    {
        cpu.ReadyForNextInstruction();
        _ = cpu.ReadByteFromMemory(cpu.ReadByteFromProgramCounter());

        var pc = cpu.GetFlags() | ProcessorStatus.B;
        cpu.WriteByteToStackMemory((byte)pc);
    }
}