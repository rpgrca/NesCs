namespace NesCs.Logic.Cpu.Instructions;

public class PullProcessorStatusOpcode28 : IInstruction
{
    public string Name => "PLP";

    public byte Opcode => 0x28;

    public byte[] PeekOperands(Cpu6502 cpu) => Array.Empty<byte>();

    public void Execute(Cpu6502 cpu)
    {
        cpu.ReadyForNextInstruction();
        _ = cpu.ReadByteFromMemory(cpu.ReadByteFromProgramCounter());

        _ = cpu.ReadByteFromStackMemory();
        var sp = cpu.ReadByteFromStackPointer();
        sp += 1;
        cpu.SetValueToStackPointer(sp);

        var pc = (ProcessorStatus)cpu.ReadByteFromStackMemory() & ~ProcessorStatus.B | ProcessorStatus.X;
        cpu.OverwriteFlags(pc);
    }
}