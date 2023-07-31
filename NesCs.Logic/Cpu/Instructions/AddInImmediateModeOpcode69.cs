namespace NesCs.Logic.Cpu.Instructions;

public class AddInImmediateModeOpcode69 : MathImmediateMode
{
    protected override (int, byte) ExecuteOperation(Cpu6502 cpu, byte accumulator, byte value) =>
        (accumulator + value + (cpu.ReadCarryFlag()? 1 : 0), value);
}