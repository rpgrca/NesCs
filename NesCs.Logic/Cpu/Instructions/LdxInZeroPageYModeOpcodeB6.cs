namespace NesCs.Logic.Cpu.Instructions;

public class LdxInZeroPageYModeOpcodeB6 : IInstruction
{
    public void Execute(Cpu6502 cpu)
    {
        cpu.ReadyForNextInstruction();
        var address = cpu.ReadByteFromProgram();
        _ = cpu.ReadByteFromMemory(address);

        cpu.ReadyForNextInstruction();
        var a = cpu.ReadByteFromMemory((byte)(address + cpu.ReadByteFromRegisterY()));
        cpu.SetValueIntoRegisterX(a);

        cpu.SetZeroFlagBasedOnRegisterX();
        cpu.SetNegativeFlagBasedOnRegisterX();
    }
}