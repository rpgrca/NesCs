namespace NesCs.Logic.Cpu.Instructions;

public class BitTestZeroPageModeOpcode24 : IInstruction
{
    public void Execute(Cpu6502 cpu)
    {
        cpu.ReadyForNextInstruction();
        var address = cpu.ReadByteFromProgram();

        cpu.ReadyForNextInstruction();
        var value = cpu.ReadByteFromMemory(address);

        var result = (byte)(value & cpu.ReadByteFromAccumulator());
        cpu.SetZeroFlagBasedOn(result);
        cpu.SetNegativeFlagBasedOn(value);
        cpu.SetOverflowFlagBasedOn(value);
    }
}