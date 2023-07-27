namespace NesCs.Logic.Cpu.Instructions;

public class LdaInZeroPageXModeOpcodeB5 : IInstruction
{
    public void Execute(Cpu6502 cpu)
    {
        cpu.ReadyForNextInstruction();
        var address = cpu.ReadByteFromProgram();
        _ = cpu.ReadByteFromMemory(address);

        cpu.ReadyForNextInstruction();
        var a = cpu.ReadByteFromMemory((byte)(address + cpu.ReadByteFromRegisterX()));
        cpu.SetValueIntoAccumulator(a);

        cpu.SetZeroFlagBasedOnAccumulator();
        cpu.SetNegativeFlagBasedOnAccumulator();
    }
}