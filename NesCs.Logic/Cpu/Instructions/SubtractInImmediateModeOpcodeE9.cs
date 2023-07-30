using static NesCs.Logic.Cpu.Cpu6502;

namespace NesCs.Logic.Cpu.Instructions;

public class SubtractInImmediateModeOpcodeE9 : IInstruction
{
    public void Execute(Cpu6502 cpu)
    {
        cpu.ReadyForNextInstruction();
        var value = cpu.ReadByteFromProgram();

        cpu.ReadyForNextInstruction();
        value = (byte)(cpu.ReadByteFromAccumulator() - (value - ~cpu.ReadCarryFlag()));
        cpu.SetValueIntoAccumulator(value);

        if (((ProcessorStatus)value & ProcessorStatus.V) == ProcessorStatus.V)
        {
            cpu.ClearCarryFlag();
        }

        cpu.SetZeroFlagBasedOn(value);
        cpu.SetNegativeFlagBasedOn(value);
    }
}