namespace NesCs.Logic.Cpu.Instructions;

public class IllegalLaxDirectYOpcodeB7 : IInstruction
{
    public void Execute(Cpu6502 cpu)
    {
        cpu.ReadyForNextInstruction();
        var address = cpu.ReadByteFromProgram();
        _ = cpu.ReadByteFromMemory(address);

        address = (byte)(address + cpu.ReadByteFromRegisterY());
        cpu.ReadyForNextInstruction();
        var value = cpu.ReadByteFromMemory(address);

        cpu.SetValueToAccumulator(value);
        cpu.SetValueToRegisterX(value);

        cpu.SetZeroFlagBasedOn(value);
        cpu.SetNegativeFlagBasedOn(value);
    }
}