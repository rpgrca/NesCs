namespace NesCs.Logic.Cpu.Instructions;

public class LdaInImmediateModeOpcodeA9 : IInstruction
{
    private readonly IInstruction _ldInImmediateMode;

    public LdaInImmediateModeOpcodeA9() =>
        _ldInImmediateMode = new LoadInImmediateMode((c, v) => c.SetValueIntoAccumulator(v));

    public void Execute(Cpu6502 cpu) => _ldInImmediateMode.Execute(cpu);
}