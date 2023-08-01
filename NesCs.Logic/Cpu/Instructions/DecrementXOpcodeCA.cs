namespace NesCs.Logic.Cpu.Instructions;

public class DecrementXOpcodeCA : IInstruction
{
    public void Execute(Cpu6502 cpu)
    {
        cpu.ReadyForNextInstruction();
        var x = (byte)(cpu.ReadByteFromRegisterX() - 1);

        _ = cpu.ReadByteFromMemory(cpu.ReadByteFromProgramCounter());
        cpu.SetValueIntoRegisterX(x);

        cpu.SetZeroFlagBasedOn(x);
        cpu.SetNegativeFlagBasedOn(x);
    }
}