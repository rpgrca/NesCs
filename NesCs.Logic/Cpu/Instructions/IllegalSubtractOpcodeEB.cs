namespace NesCs.Logic.Cpu.Instructions;

public class IllegalSubtractOpcodeEB : MathImmediateMode
{
    public override string Name => "SBC";

    public override byte Opcode => 0xEB;

    protected override (int, byte) ExecuteOperation(Cpu6502 cpu, byte accumulator, byte value)
    {
        value = (byte)~value;
        return (accumulator + value + (cpu.IsReadCarryFlagSet()? 1 : 0), value);
    }
}