namespace NesCs.Logic.Cpu.Instructions.Modes;

public class IndirectXMode : IInstruction
{
    public void Execute(Cpu6502 cpu)
    {
        cpu.ReadyForNextInstruction();
        var address = cpu.ReadByteFromProgram();
        _ = cpu.ReadByteFromMemory(address);

        address = (byte)(address + cpu.ReadByteFromRegisterX());
        cpu.ReadyForNextInstruction();
        var low = cpu.ReadByteFromMemory(address);

        address = (byte)(address + 1);
        var high = cpu.ReadByteFromMemory(address);

        var effectiveAddress = high << 8 | low;
        var value = cpu.ReadByteFromMemory(effectiveAddress);
        value = ExecuteOperation(cpu, value);
        StoreValueInFinalDestination(cpu, value);

        cpu.SetZeroFlagBasedOnAccumulator();
        cpu.SetNegativeFlagBasedOnAccumulator();
    }

    protected virtual byte ExecuteOperation(Cpu6502 cpu, byte value) => value;

    protected virtual void StoreValueInFinalDestination(Cpu6502 cpu, byte value) =>
        cpu.SetValueIntoAccumulator(value);
}