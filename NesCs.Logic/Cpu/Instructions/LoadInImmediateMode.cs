namespace NesCs.Logic.Cpu.Instructions;

public class LoadInImmediateMode : IInstruction
{
    private readonly Action<Cpu6502, byte> _setValue;

    public LoadInImmediateMode(Action<Cpu6502, byte> setValue) => _setValue = setValue;

    public void Execute(Cpu6502 cpu)
    {
        cpu.ReadyForNextInstruction();
        var value = cpu.ReadByteFromProgram();

        cpu.ReadyForNextInstruction();
        _setValue(cpu, value);

        cpu.SetZeroFlagBasedOn(value);
        cpu.SetNegativeFlagBasedOn(value);
    }
}