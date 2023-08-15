namespace NesCs.Logic.Cpu.Instructions;

public class PushProcessorStatusOpcode08 : IInstruction
{
    public string Name => "PHP";

    public byte Opcode => 0x08;

    public byte[] PeekOperands(Cpu6502 cpu) => Array.Empty<byte>();

    public void Execute(Cpu6502 cpu)
    {
        cpu.ReadyForNextInstruction();
        _ = cpu.ReadByteFromMemory(cpu.ReadByteFromProgramCounter());

        var pc = cpu.GetFlags() | ProcessorStatus.B;
        cpu.WriteByteToStackMemory((byte)pc);
    }
}