namespace NesCs.Logic.Cpu.Instructions;

public class LdxInImmediateModeOpcodeA2 : IInstruction
{
    private readonly IInstruction _loadInImmediateMode;

    public LdxInImmediateModeOpcodeA2() =>
        _loadInImmediateMode = new LoadInImmediateMode((c, v) => c.SetValueIntoRegisterX(v));

    public void Execute(Cpu6502 cpu) => _loadInImmediateMode.Execute(cpu);
}