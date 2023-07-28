namespace NesCs.Logic.Cpu.Instructions;

public class LdyInImmediateModeOpcodeA0 : IInstruction
{
    private readonly IInstruction _loadInImmediateMode;

    public LdyInImmediateModeOpcodeA0() =>
        _loadInImmediateMode = new LoadInImmediateMode((c, v) => c.SetValueIntoRegisterY(v));

    public void Execute(Cpu6502 cpu) => _loadInImmediateMode.Execute(cpu);
}