namespace NesCs.Logic.Cpu.Instructions;

public class IncrementXOpcodeE8 : IInstruction
{
    public void Execute(Cpu6502 cpu)
    {
        cpu.ReadyForNextInstruction();
        var x = (byte)(cpu.ReadByteFromRegisterX() + 1);

        _ = cpu.ReadByteFromMemory(cpu.ReadByteFromProgramCounter());
        cpu.SetValueToRegisterX(x);

        cpu.SetZeroFlagBasedOn(x);
        cpu.SetNegativeFlagBasedOn(x);
    }
}