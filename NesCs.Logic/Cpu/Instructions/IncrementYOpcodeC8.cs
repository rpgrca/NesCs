namespace NesCs.Logic.Cpu.Instructions;

public class IncrementYOpcodeC8 : IInstruction
{
    public void Execute(Cpu6502 cpu)
    {
        cpu.ReadyForNextInstruction();
        var y = (byte)(cpu.ReadByteFromRegisterY() + 1);

        _ = cpu.ReadByteFromMemory(cpu.ReadByteFromProgramCounter());
        cpu.SetValueIntoRegisterY(y);

        cpu.SetZeroFlagBasedOn(y);
        cpu.SetNegativeFlagBasedOn(y);
    }
}