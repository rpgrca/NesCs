namespace NesCs.Logic.Cpu.Instructions;

// Covers opcode 0xB9 and 0xBD
public class LdaInAbsoluteIndexedMode : IInstruction
{
    private readonly Func<Cpu6502, byte> _indexRegister;

    public LdaInAbsoluteIndexedMode(Func<Cpu6502, byte> indexRegister) => _indexRegister = indexRegister;

    public void Execute(Cpu6502 cpu)
    {
        cpu.ReadyForNextInstruction();
        var low = cpu.ReadByteFromProgram();

        cpu.ReadyForNextInstruction();
        var high = cpu.ReadByteFromProgram();

        cpu.ReadyForNextInstruction();
        var address = (high << 8) | (low + _indexRegister(cpu)) & 0xff;
        var a = cpu.ReadByteFromMemory(address);
        cpu.SetValueIntoAccumulator(a);

        var address2 = (((high << 8) | low) + _indexRegister(cpu)) & 0xffff;
        if (address != address2)
        {
            a = cpu.ReadByteFromMemory(address2);
            cpu.SetValueIntoAccumulator(a);
        }

        cpu.SetZeroFlagBasedOnAccumulator();
        cpu.SetNegativeFlagBasedOnAccumulator();
    }
}