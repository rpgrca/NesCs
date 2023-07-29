using static NesCs.Logic.Cpu.Cpu6502;

namespace NesCs.Logic.Cpu.Instructions;

public class BitTestAbsoluteOpcode2C : IInstruction
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

        var result = (byte)(value & cpu.ReadByteFromAccumulator());
        cpu.SetZeroFlagBasedOn(result);
        cpu.SetNegativeFlagBasedOn(value);
        cpu.SetOverflowFlagBasedOn(value);
    }
}