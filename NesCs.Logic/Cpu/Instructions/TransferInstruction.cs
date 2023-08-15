namespace NesCs.Logic.Cpu.Instructions;

public abstract class TransferInstruction : IInstruction
{
    public void Execute(Cpu6502 cpu)
    {
        var value = ObtainValueFromSource(cpu);

        cpu.ReadyForNextInstruction();
        _ = cpu.ReadByteFromMemory(cpu.ReadByteFromProgramCounter());

        StoreValueInFinalDestination(cpu, value);

        cpu.SetZeroFlagBasedOn(value);
        cpu.SetNegativeFlagBasedOn(value);
    }

    protected abstract byte ObtainValueFromSource(Cpu6502 cpu);

    public abstract byte Opcode { get; }

    public abstract string Name { get; }

    protected abstract void StoreValueInFinalDestination(Cpu6502 cpu, byte value);

    public byte[] PeekOperands(Cpu6502 cpu) => Array.Empty<byte>();
}