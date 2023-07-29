namespace NesCs.Logic.Cpu.Instructions;

public class AndInIndirectXModeOpcode21 : IInstruction
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

        var effectiveAddress = (high << 8) | low;
        var a = cpu.ReadByteFromMemory(effectiveAddress);
        a = (byte)(a & cpu.ReadByteFromAccumulator());
        cpu.SetValueIntoAccumulator(a);

        cpu.SetZeroFlagBasedOnAccumulator();
        cpu.SetNegativeFlagBasedOnAccumulator();
    }
}