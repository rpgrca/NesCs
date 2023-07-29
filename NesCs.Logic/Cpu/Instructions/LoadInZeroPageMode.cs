namespace NesCs.Logic.Cpu.Instructions;

public abstract class ZeroPageMode : IInstruction
{
    public void Execute(Cpu6502 cpu)
    {
        cpu.ReadyForNextInstruction();
        var address = cpu.ReadByteFromProgram();

        cpu.ReadyForNextInstruction();
        var value = cpu.ReadByteFromMemory(address);
        value = ExecuteOperation(cpu, value);
        StoreValueInFinalDestination(cpu, value);

        cpu.SetZeroFlagBasedOn(value);
        cpu.SetNegativeFlagBasedOn(value);
    }

    protected virtual void StoreValueInFinalDestination(Cpu6502 cpu, byte value) =>
        cpu.SetValueIntoAccumulator(value);

    protected virtual byte ExecuteOperation(Cpu6502 cpu, byte value) => value;
}