namespace NesCs.Logic.Cpu.Instructions;

public abstract class LoadInZeroIndexedMode : IInstruction
{
    public void Execute(Cpu6502 cpu)
    {
        cpu.ReadyForNextInstruction();
        var address = cpu.ReadByteFromProgram();
        _ = cpu.ReadByteFromMemory(address);

        cpu.ReadyForNextInstruction();
        var value = cpu.ReadByteFromMemory((byte)(address + ObtainValueForIndex(cpu)));
        StoreValueInFinalDestination(cpu, value);

        cpu.SetZeroFlagBasedOn(value);
        cpu.SetNegativeFlagBasedOn(value);
    }

    protected abstract void StoreValueInFinalDestination(Cpu6502 cpu, byte value);

    protected abstract byte ObtainValueForIndex(Cpu6502 cpu);
}