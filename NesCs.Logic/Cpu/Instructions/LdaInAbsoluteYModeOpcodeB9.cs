namespace NesCs.Logic.Cpu.Instructions;

public class LdaInAbsoluteYModeOpcodeB9 : IInstruction
{
    private readonly IInstruction _ldaInAbsoluteMode;

    public LdaInAbsoluteYModeOpcodeB9() =>
        _ldaInAbsoluteMode = new LdaInAbsoluteIndexedMode(c => c.ReadByteFromRegisterY());

    public void Execute(Cpu6502 cpu) => _ldaInAbsoluteMode.Execute(cpu);
}