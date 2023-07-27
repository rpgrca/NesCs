namespace NesCs.Logic.Cpu.Instructions;

public class LdaInAbsoluteXModeOpcodeBD : IInstruction
{
    private readonly IInstruction _ldaInAbsoluteMode;

    public LdaInAbsoluteXModeOpcodeBD() =>
        _ldaInAbsoluteMode = new LdaInAbsoluteIndexedMode(c => c.ReadByteFromRegisterX());

    public void Execute(Cpu6502 cpu) => _ldaInAbsoluteMode.Execute(cpu);
}