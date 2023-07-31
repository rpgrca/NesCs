using static NesCs.Logic.Cpu.Cpu6502;

namespace NesCs.Logic.Cpu.Instructions;

public class SubtractInImmediateModeOpcodeE9 : MathImmediateMode
{
    protected override (int, byte) ExecuteOperation(Cpu6502 cpu, byte accumulator, byte value)
    {
        value = (byte)~value;
        return (accumulator + value + (cpu.ReadCarryFlag()? 1 : 0), value);
    }
}