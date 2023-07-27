namespace NesCs.Logic.Cpu.Instructions;

public class LdxInImmediateModeOpcodeA2 : IInstruction
{
    private readonly IInstruction _ldInImmediateMode;

    public LdxInImmediateModeOpcodeA2() =>
        _ldInImmediateMode = new LoadInImmediateMode((c, v) => c.SetValueIntoRegisterX(v));

    public void Execute(Cpu6502 cpu) => _ldInImmediateMode.Execute(cpu);
}