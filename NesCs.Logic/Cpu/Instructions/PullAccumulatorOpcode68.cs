namespace NesCs.Logic.Cpu.Instructions;

public class PullAccumulatorOpcode68 : IInstruction
{
    public string Name => "PLA";

    public byte Opcode => 0x68;

    public byte[] PeekOperands(Cpu6502 cpu) => Array.Empty<byte>();

    public void Execute(Cpu6502 cpu)
    {
        cpu.ReadyForNextInstruction();
        _ = cpu.ReadByteFromMemory(cpu.ReadByteFromProgramCounter());

        _ = cpu.ReadByteFromStackMemory();
        var sp = cpu.ReadByteFromStackPointer();
        sp += 1;
        cpu.SetValueToStackPointer(sp);

        var a = cpu.ReadByteFromStackMemory();
        cpu.SetValueToAccumulator(a);

        cpu.SetZeroFlagBasedOn(a);
        cpu.SetNegativeFlagBasedOn(a);
    }
}