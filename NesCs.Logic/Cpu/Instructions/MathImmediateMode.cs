namespace NesCs.Logic.Cpu.Instructions;

public abstract class MathImmediateMode : IInstruction
{
    public void Execute(Cpu6502 cpu)
    {
        cpu.ReadyForNextInstruction();
        var v = cpu.ReadByteFromProgram();

        cpu.ReadyForNextInstruction();
        var a = cpu.ReadByteFromAccumulator();

        var (sum, value) = ExecuteOperation(cpu, a, v);
        var result = (byte)(sum & 0xff);

        cpu.SetValueToAccumulator(result);
        cpu.ClearCarryFlag();
        cpu.ClearNegativeFlag();
        cpu.ClearOverflowFlag();
        cpu.ClearZeroFlag();

        // TODO: Not a real 8-bit implementation but works for the time being
        if ((sum >> 8) != 0)
        {
            cpu.SetCarryFlag();
        }

        if (((a ^ result) & (value ^ result) & 0x80) != 0)
        {
            cpu.SetOverflowFlag();
        }

        cpu.SetZeroFlagBasedOn(result);
        cpu.SetNegativeFlagBasedOn(result);
    }

    protected abstract (int, byte) ExecuteOperation(Cpu6502 cpu, byte accumulator, byte value);
}
