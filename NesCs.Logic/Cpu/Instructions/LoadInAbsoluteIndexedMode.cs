namespace NesCs.Logic.Cpu.Instructions;

public abstract class LoadInAbsoluteIndexedMode : IInstruction
{
    public void Execute(Cpu6502 cpu)
    {
        cpu.ReadyForNextInstruction();
        var low = cpu.ReadByteFromProgram();

        cpu.ReadyForNextInstruction();
        var high = cpu.ReadByteFromProgram();

        cpu.ReadyForNextInstruction();
        var address = (high << 8) | (low + ObtainValueForIndex(cpu)) & 0xff;
        var value = cpu.ReadByteFromMemory(address);

        var address2 = (((high << 8) | low) + ObtainValueForIndex(cpu)) & 0xffff;
        if (address != address2)
        {
            value = cpu.ReadByteFromMemory(address2);
        }

        value = ExecuteOperation(cpu, value);
        StoreValueInFinalDestination(cpu, value);
        cpu.SetZeroFlagBasedOn(value);
        cpu.SetNegativeFlagBasedOn(value);
    }

    protected abstract byte ObtainValueForIndex(Cpu6502 cpu);

    protected abstract byte ExecuteOperation(Cpu6502 cpu, byte value);

    protected abstract void StoreValueInFinalDestination(Cpu6502 cpu, byte value);
}