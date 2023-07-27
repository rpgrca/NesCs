namespace NesCs.Logic.Cpu.Instructions;

public class LdxInImmediateModeOpcodeA2 : IInstruction
{
    public void Execute(Cpu6502 cpu)
    {
        cpu.ReadyForNextInstruction();
        var value = cpu.ReadByteFromProgram();

        cpu.ReadyForNextInstruction();
        cpu.SetValueIntoRegisterX(value);

        cpu.SetZeroFlagBasedOnRegisterX();
        cpu.SetNegativeFlagBasedOnRegisterX();
    }
}