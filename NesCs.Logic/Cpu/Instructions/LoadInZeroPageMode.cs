namespace NesCs.Logic.Cpu.Instructions;

public abstract class LoadInZeroPageMode : IInstruction
{
    public void Execute(Cpu6502 cpu)
    {
        cpu.ReadyForNextInstruction();
        var address = cpu.ReadByteFromProgram();

        cpu.ReadyForNextInstruction();
        var value = cpu.ReadByteFromMemory(address);
        StoreValueInFinalDestination(cpu, value);

        cpu.SetZeroFlagBasedOn(value);
        cpu.SetNegativeFlagBasedOn(value);
    }

    protected abstract void StoreValueInFinalDestination(Cpu6502 cpu, byte value);
}