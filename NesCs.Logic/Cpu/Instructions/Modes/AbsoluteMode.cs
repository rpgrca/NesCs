namespace NesCs.Logic.Cpu.Instructions.Modes;

public abstract class AbsoluteMode : IInstruction
{
    public void Execute(Cpu6502 cpu)
    {
        cpu.ReadyForNextInstruction();
        var low = cpu.ReadByteFromProgram();

        cpu.ReadyForNextInstruction();
        var high = cpu.ReadByteFromProgram();

        cpu.ReadyForNextInstruction();
        var address = high << 8 | low;
        var value = cpu.ReadByteFromMemory(address);

        value = ExecuteOperation(cpu, value);
        StoreValueInFinalDestination(cpu, value);

        cpu.SetZeroFlagBasedOn(value);
        cpu.SetNegativeFlagBasedOn(value);
    }

    protected virtual void StoreValueInFinalDestination(Cpu6502 cpu, byte value) =>
        cpu.SetValueToAccumulator(value);

    protected virtual byte ExecuteOperation(Cpu6502 cpu, byte value) => value;
}