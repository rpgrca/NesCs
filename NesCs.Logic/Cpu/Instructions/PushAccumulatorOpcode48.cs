namespace NesCs.Logic.Cpu.Instructions;

public class PushAccumulatorOpcode48 : IInstruction
{
    public string Name => "PHA";

    public byte Opcode => 0x48;

    public byte[] PeekOperands(Cpu6502 cpu) => Array.Empty<byte>();

    public void Execute(Cpu6502 cpu)
    {
        cpu.ReadyForNextInstruction();
        _ = cpu.ReadByteFromMemory(cpu.ReadByteFromProgramCounter());

        cpu.WriteByteToStackMemory(cpu.ReadByteFromAccumulator());
    }
}