namespace NesCs.Logic.Cpu.Instructions;

public class SubtractInImmediateModeOpcodeE9 : MathImmediateMode
{
    protected override (int, byte) ExecuteOperation(Cpu6502 cpu, byte accumulator, byte value)
    {
        value = (byte)~value;
        return (accumulator + value + (cpu.IsReadCarryFlagSet()? 1 : 0), value);
    }
}