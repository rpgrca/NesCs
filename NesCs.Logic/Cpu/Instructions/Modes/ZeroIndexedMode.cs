namespace NesCs.Logic.Cpu.Instructions;

public abstract class ZeroIndexedMode : IInstruction
{
    public void Execute(Cpu6502 cpu)
    {
        cpu.ReadyForNextInstruction();
        var address = cpu.ReadByteFromProgram();
        _ = cpu.ReadByteFromMemory(address);

        cpu.ReadyForNextInstruction();
        var value = cpu.ReadByteFromMemory((byte)(address + ObtainValueForIndex(cpu)));
        value = ExecuteOperation(cpu, value);
        StoreValueInFinalDestination(cpu, value);

        cpu.SetZeroFlagBasedOn(value);
        cpu.SetNegativeFlagBasedOn(value);
    }

    protected abstract byte ObtainValueForIndex(Cpu6502 cpu);

    protected virtual void StoreValueInFinalDestination(Cpu6502 cpu, byte value) =>
        cpu.SetValueIntoAccumulator(value);

    protected virtual byte ExecuteOperation(Cpu6502 cpu, byte value) => value;
}